using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EditorData
{
    private static GameObject m_selected_Tile;

    public static GameObject Selected_Tile
    {
        get
        {
            return m_selected_Tile;
        }
        set
        {
            m_selected_Tile = value;
        }
    }
}
