using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class GridNote
{
    [SerializeField] private Vector3Int gridPosition;
    [SerializeField] private TileData tile;
    [HideInInspector]
    [SerializeField] private GameObject currentPrefab;
    [HideInInspector]
    [SerializeField] private GameObject instance;

    public Vector3Int GridPosition { get => gridPosition; set => gridPosition = value; }
    public TileData Tile { get => tile; set => tile = value; }

    public GridNote(Vector3Int gridPosition, TileData tile)
    {
        this.gridPosition = gridPosition;
        this.tile = tile;
    }

    public void Update(GridNote[] notes, Transform parent, int gridSize)
    {
        GameObject prefab = tile.GetTile(gridPosition, notes);

        if (prefab != currentPrefab)
        {
            currentPrefab = prefab;

            if (instance != null)
            {
                Undo.DestroyObjectImmediate(instance);
            }

            instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent);
            Undo.RegisterCreatedObjectUndo(instance, "tile spawn");
            instance.transform.position = gridPosition * gridSize;
        }
    }

    public void Dispatch()
    {
        if (instance != null)
        {
            Undo.DestroyObjectImmediate(instance);
        }
    }
}
