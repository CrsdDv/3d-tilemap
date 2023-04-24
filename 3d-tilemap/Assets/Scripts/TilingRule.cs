using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "new Tiling rule", menuName = "3d Tilemap/Tiling rule")]
public class TilingRule : ScriptableObject
{
    public bool m_left;
    public bool m_above;
    public bool m_below;
    public bool m_inFront;
    public bool m_behind;
    public bool m_right;

    private int rulePartCount;

    public bool IsMet(Vector3Int gridPostion, GridNote[] notes)
    {
        int rulesMetCount = 0;

        foreach (var note in notes)
        {
            int neighborState = IsNoteNeighbor(gridPostion, note.GridPosition);

            bool isRule = false;

            switch (neighborState)
            {
                case 0: // Left
                    {
                        isRule = m_left;
                        break;
                    }
                case 1: // Right
                    {
                        isRule = m_right;
                        break;
                    }
                case 2: // Above
                    {
                        isRule = m_above;
                        break;
                    }
                case 3: // Below
                    {
                        isRule = m_below;
                        break;
                    }
                case 4: // Infront
                    {
                        isRule = m_inFront;
                        break;
                    }
                case 5: // Behind
                    {
                        isRule = m_behind;
                        break;
                    }
            }

            rulesMetCount += isRule ? 1 : 0;
        }

        return rulesMetCount == rulePartCount;
    }

    private int IsNoteNeighbor(Vector3Int gridPos, Vector3Int neighborPos)
    {
        if (neighborPos == gridPos + Vector3.left)
        {
            return 0;
        }
        if (neighborPos == gridPos + Vector3.right)
        {
            return 1;
        }
        if (neighborPos == gridPos + Vector3.up)
        {
            return 2;
        }
        if (neighborPos == gridPos + Vector3.down)
        {
            return 3;
        }
        if (neighborPos == gridPos + Vector3.forward)
        {
            return 4;
        }
        if (neighborPos == gridPos + Vector3.back)
        {
            return 5;
        }

        return -1;
    }

    private void OnValidate()
    {
        rulePartCount = 0;
        if (m_left)
        {
            rulePartCount++;
        }
        if (m_right)
        {
            rulePartCount++;
        }
        if (m_inFront)
        {
            rulePartCount++;
        }
        if (m_behind)
        {
            rulePartCount++;
        }
        if (m_above)
        {
            rulePartCount++;
        }
        if (m_below)
        {
            rulePartCount++;
        }
    }
}
