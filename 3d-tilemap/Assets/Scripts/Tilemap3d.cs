using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class Tilemap3d : MonoBehaviour
{
    [SerializeField] private int gridSize;
    [SerializeField] private List<GridNote> gridNotes;

    public List<GridNote> GridNotes { get => gridNotes; }
    public int GridSize { get => gridSize; }

    public void AddGridNote(Vector3Int gridPosition, TileData tile)
    {
        if (gridNotes == null)
        {
            gridNotes = new List<GridNote>();
        }

        GridNote[] notesOnPosition = gridNotes.Where(t => t.GridPosition == gridPosition).ToArray();

        Undo.RecordObject(this, "update tile data");
        if (notesOnPosition.Length != 0)
        {
            notesOnPosition[0].Tile = tile;
        }
        else
        {
            GridNote note = new GridNote(gridPosition, tile);
            gridNotes.Add(note);
        }


        RecalcMap();
    }

    public void RemoveGridNote(Vector3Int gridPosition)
    {
        if (gridNotes == null)
        {
            return;
        }


        foreach (var note in gridNotes)
        {
            if (note.GridPosition == gridPosition)
            {
                Undo.RecordObject(this, "update tile data");
                note.Dispatch();
                gridNotes.Remove(note);
                break;
            }
        }

        RecalcMap();
    }

    private void RecalcMap()
    {
        Transform mapTransform = this.transform;

        foreach (var note in gridNotes)
        {
            note.Update(gridNotes.ToArray(), mapTransform, gridSize);
        }
    }
}
