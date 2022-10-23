using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using UnityEditor.ShortcutManagement;


public class TileMapEditorWindow : EditorWindow
{

    private int selected_Tool = -1;
    private TilePalette m_TilePalette;

    private Vector2 m_scrollPosition;
    private int m_selectedTile;

    [MenuItem("MyTool/TileMapEditor")]
    public static void OpenTileMapEditor()
    {
        var wnd = GetWindow<TileMapEditorWindow>();
        wnd.titleContent = new GUIContent("TileMapEditor");
        
    }

    
    //gui event handling and redering

    private void OnGUI()
    {


        EditorGUI.BeginChangeCheck();

        using (new GUILayout.HorizontalScope(GUI.skin.box))
        {
            //toolbar
            //GUIContent(text, tooltip)
            GUIContent[] GUI_Contents = { new GUIContent("default"),
            new GUIContent("Pencil","draw tile mode"), new GUIContent("Erase")};
            selected_Tool = GUILayout.Toolbar(selected_Tool, GUI_Contents);
        }

        EditorGUILayout.Space();

        //Tile Prefab
        //var default_palette = AssetDatabase.LoadAssetAtPath<TilePalette>("Assets/Palette/NewPalette.asset");
        m_TilePalette = EditorGUILayout.ObjectField("TilePalette", m_TilePalette, typeof(TilePalette), false) as TilePalette;

        if(m_TilePalette != null)
        {
            //tile prefab list draw
            if(m_TilePalette.m_prfabs_List.Count > 0)
            {
                //scrollview & grid

                Texture[] prefab_textures = m_TilePalette.m_prfabs_List.Select(pv => AssetPreview.GetAssetPreview(pv)).ToArray();
                
                using (EditorGUILayout.ScrollViewScope scrollView = new EditorGUILayout.ScrollViewScope(m_scrollPosition))
                {
                    m_scrollPosition = scrollView.scrollPosition;

                    //GUILayout.SelectionGrid(선택된 타일, 그려질 텍스쳐, 가로에 들어갈 갯수, 그리드뷰 width사이즈, 그리드뷰 height 사이즈)
                    //width, height를 윈도우 사이즈 크기 변화에 맞춘다.
                    m_selectedTile = GUILayout.SelectionGrid(m_selectedTile, prefab_textures, 4, GUILayout.Width(position.width - 20),
                        GUILayout.Height((position.width - 20) / 4 * Mathf.CeilToInt((float)prefab_textures.Length / 4)));


                }
            }

        }

        // End the code block and update gui change occurred
        if (EditorGUI.EndChangeCheck())
        {
            //선택된 타일 정보 가지고 있기
            EditorData.Selected_Tile = m_TilePalette.m_prfabs_List[m_selectedTile];
            
        }
    }

    private void OnDestroy()
    {
        EditorData.Selected_Tile = null;

        //tilemap object 찾아서 에디터윈도우 종료시 초기화
        TileMap[] tileMaps = FindObjectsOfType(typeof(TileMap)) as TileMap[];

        foreach(var chlid in tileMaps)
        {
            chlid.Reset();
        }
    }
}
