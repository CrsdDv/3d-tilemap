using Codice.Client.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;

[EditorTool("Tilemap 3D Tool", typeof(Tilemap3d))]
public class Tilemap3dTool : EditorTool, IDrawSelectedHandles
{
    private GUIContent m_ToolbarIcon;
    public TileData data;
    private Tilemap3d targetTilemap;

    void OnEnable()
    {
        // Allocate unmanaged resources or perform one-time set up functions here
    }

    void OnDisable()
    {
        // Free unmanaged resources, state teardown.
    }

    public override GUIContent toolbarIcon
    {
        get
        {
            return new GUIContent(AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/tilemap_3d_icon.png"),
                "Tilemap 3d Tool");
        }
    }

    public override void OnActivated()
    {
        SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Entering Tilemap Tool"), .1f);

        targetTilemap = (Tilemap3d)target;
    }

    public override void OnWillBeDeactivated()
    {
        SceneView.lastActiveSceneView.ShowNotification(new GUIContent("Exiting Tilemap Tool"), .1f);
    }

    public override void OnToolGUI(EditorWindow window)
    {
        if (!(window is SceneView sceneView))
        {
            return;
        }

        EditorGUI.BeginChangeCheck();

        Handles.color = Color.green;

        if (targetTilemap.GridNotes == null || targetTilemap.GridNotes.Count == 0)
        {
            if (Handles.Button(Vector3.zero, Quaternion.identity, 0.5f, 0.5f, Handles.CubeHandleCap))
                targetTilemap.AddGridNote(Vector3Int.zero, data);
        }

        foreach (var item in targetTilemap.GridNotes)
        {
            Vector3Int gridPos = item.GridPosition;

            Handles.color = Color.green;
            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

            Vector3Int upPosition = gridPos + Vector3Int.up;
            Vector3Int downPosition = gridPos + Vector3Int.down;
            Vector3Int leftPosition = gridPos + Vector3Int.left;
            Vector3Int rightPosition = gridPos + Vector3Int.right;
            Vector3Int forwardPosition = gridPos + Vector3Int.forward;
            Vector3Int backPosition = gridPos + Vector3Int.back;

            if (DrawButton(upPosition))
            {
                targetTilemap.AddGridNote(upPosition, data);
                break;
            }
            if (DrawButton(downPosition))
            {
                targetTilemap.AddGridNote(downPosition, data);
                break;
            }
            if (DrawButton(leftPosition))
            {
                targetTilemap.AddGridNote(leftPosition, data);
                break;
            }
            if (DrawButton(rightPosition))
            {
                targetTilemap.AddGridNote(rightPosition, data);
                break;
            }
            if (DrawButton(forwardPosition))
            {
                targetTilemap.AddGridNote(forwardPosition, data);
                break;
            }
            if (DrawButton(backPosition))
            {
                targetTilemap.AddGridNote(backPosition, data);
                break;
            }
        }
        foreach (var item in targetTilemap.GridNotes)
        {
            Vector3Int gridPos = item.GridPosition;

            Handles.color = Color.red;
            Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
            if (Handles.Button(gridPos * targetTilemap.GridSize, Quaternion.identity, 0.25f, 0.25f, Handles.CubeHandleCap))
            {
                targetTilemap.RemoveGridNote(gridPos);
                break;
            }
        }
    }

    private bool DrawButton(Vector3 pos)
    {
        return Handles.Button(pos * targetTilemap.GridSize, Quaternion.identity, 0.2f, 0.2f, Handles.CubeHandleCap);
    }

    public void OnDrawHandles()
    {
        
    }
}
