using UnityEngine;

[CreateAssetMenu(fileName = "new Tile data", menuName = "3d Tilemap/Tile data")]
public class TileData : ScriptableObject
{
    [System.Serializable]
    private class Tile
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private TilingRule rule;

        public GameObject Prefab { get => prefab; }
        public TilingRule Rule { get => rule; }
    }

    [SerializeField] private GameObject defaultTile;
    [SerializeField] private Tile[] tiles;

    public GameObject GetTile(Vector3Int gridPostion, GridNote[] notes)
    {
        foreach (var tile in tiles)
        {
            if (tile.Rule.IsMet(gridPostion, notes))
            {
                return tile.Prefab;
            }
        }

        return defaultTile;
    }
}
