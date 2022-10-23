using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


[ExecuteInEditMode]//편집모드에서도 monobehaviour 이벤트 발생하게 해준다.
public class TileMap : MonoBehaviour
{

    private static int cellWidth_count = 9;
    private static int cellHeight_count = 9;

    private static int cellWidth_Size = 9;
    private static int cellHeight_Size = 9;


    private float start_x;
    private float start_y;
    private float end_x;
    private float end_y;

    private Vector2Int? Selected_Tile_Pos;

    private Dictionary<Vector2Int?, GameObject> Map_Tile_Dic = new Dictionary<Vector2Int?, GameObject>();


    private void OnEnable()
    {
        
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }


    public void Reset()
    {
        foreach (var child in Map_Tile_Dic)
        {
            var temp = child.Value;
            GameObject.DestroyImmediate(temp);
        }
        Map_Tile_Dic.Clear();
    }

    private void OnDestroy()
    {
        Reset();
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        sceneView.in2DMode = true;

        

        //selected tile draw
        //모드에 따라서 그리거나 지우거나 선택하거나

        //마우스 좌표로 타일 선택
        Vector2 mousePosition = Event.current.mousePosition;
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
        mousePosition = ray.origin;


        if(Event.current.button == 0)
        {
            switch (Event.current.type)
            {
                case EventType.MouseDown:

                    //타일 좌표 가져오기
                    //left bottom 기준 (0,0)
                    Selected_Tile_Pos = Get_CellPos(mousePosition);
                    if (Selected_Tile_Pos != null)
                    {

                        //맵에디터에서 선택한 타일을 씬뷰에 생성하자
                        Create_Tile();
                    }


                    break;
            }
        }

    }

    private void Create_Tile()
    {

        //같은곳 선택하면 기존꺼는 지우고 새로 생성

        if (Map_Tile_Dic.ContainsKey(Selected_Tile_Pos.Value))
        {
            //delete
            var temp = Map_Tile_Dic[Selected_Tile_Pos.Value];

            Map_Tile_Dic.Remove(Selected_Tile_Pos.Value);

            GameObject.DestroyImmediate(temp);
        }

        if (EditorData.Selected_Tile != null)
        {
            GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(EditorData.Selected_Tile);
            obj.name = EditorData.Selected_Tile.name;
            obj.transform.localScale = new Vector3(cellWidth_Size, cellHeight_Size);
            obj.transform.parent = transform;
            obj.transform.position = Get_CellCenterPos(Selected_Tile_Pos);

            Map_Tile_Dic[Selected_Tile_Pos.Value] = obj;
        }
    }

    //draw grid view
    private void OnDrawGizmos()
    {
        //draw grid

        start_x = -(cellWidth_count * cellWidth_Size / 2.0f);
        start_y = -(cellHeight_count * cellHeight_Size / 2.0f);
        end_x = cellWidth_count * cellWidth_Size / 2.0f;
        end_y = cellHeight_count * cellHeight_Size / 2.0f;

        Gizmos.color = new Color(1, 1, 1, 0.25f);
        //draw raw
        for (int i = 0; i < cellHeight_count + 1; i++)
        {
            Gizmos.DrawLine(new Vector3(start_x, start_y + i * cellHeight_Size) + transform.position,
                new Vector3(end_x, start_y + i * cellHeight_Size) + transform.position);
        }
        //draw column
        for (int i = 0; i < cellWidth_count + 1; i++)
        {
            Gizmos.DrawLine(new Vector3(start_x + i * cellWidth_Size, start_y) + transform.position,
                new Vector3(start_x + i * cellWidth_Size, end_y) + transform.position);
        }

        //selected tile draw
        if(Selected_Tile_Pos != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(Get_CellCenterPos(Selected_Tile_Pos), new Vector3(cellWidth_Size, cellHeight_Size));
        }

    }

    private Vector2Int? Get_CellPos(Vector2 mousePos)
    {
        if (mousePos.x < start_x || mousePos.x > end_x
            || mousePos.y < start_y || mousePos.y > end_y)
        {
            return null;
        }


        int posx = (int)((mousePos.x - start_x) / cellWidth_Size);
        int posy = (int)((mousePos.y - start_y) / cellHeight_Size);

        return new Vector2Int(posx, posy);
    }

    private Vector3 Get_CellCenterPos(Vector2Int? cell)
    {
        float x = start_x + cellWidth_Size * (cell.Value.x + 1 - (1.0f / 2));
        float y = start_y + cellHeight_Size * (cell.Value.y + 1 - (1.0f / 2));

        return new Vector3(x, y);
    }
}
