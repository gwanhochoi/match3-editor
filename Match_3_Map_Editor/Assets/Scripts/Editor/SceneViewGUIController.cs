using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SceneViewGUIController
{

    private CustomGrid customgrid;
    private string width_str;
    public SceneViewGUIController()
    {
        
        width_str = "";
    }

    public void Create_CustomGrid(int width_count, int height_count, int cell_width, int cell_height)
    {
        //Debug.Log("grid create");
        customgrid = new CustomGrid();
        customgrid.Initialize(width_count, height_count, cell_width, cell_height);
        SceneView.RepaintAll();


    }

    public void DrawGUI()
    {

        if (customgrid != null)
        {
            Handles.DrawLines(customgrid.Get_RowLineVec());
            Handles.DrawLines(customgrid.Get_ColumnLineVec());
        }


        Vector2 mousePosition = Event.current.mousePosition;
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
        mousePosition = ray.origin;

        switch (Event.current.type)
        {
            case EventType.MouseDown:

                //Debug.Log("mouse clicked");
                //Debug.Log("mouse x = " + mousePosition.x);
                //Debug.Log("mouse y = " + mousePosition.y);
                //타일 좌표 받아오고 해당 위치에 타일 그리기.
                //드래그도 되게 하고
                customgrid.Get_Current_Selected_Tile_Pos(mousePosition);
                break;

            case EventType.MouseDrag:

                break;
        }

    }

    
}
