using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
//using UnityEngine.UIElements;

public class SceneViewGUIController
{

    private CustomGrid customgrid;

    private Material base_material;
    private GameObject base_tile_prefab;

    private Dictionary<Vector2Int, GameObject> m_tileObj_dic;

    private Sprite[] sprites;

    private int width_count;
    private int height_count;
    private int cell_width;
    private int cell_height;

    public SceneViewGUIController()
    {
        base_material = Resources.Load<Material>("Materials/base_material");
        m_tileObj_dic = new Dictionary<Vector2Int, GameObject>();
        sprites = Resources.LoadAll<Sprite>("Terrain_Tiles");
        base_tile_prefab = Resources.Load<GameObject>("Prefabs/BaseTile_Cube");
    }

    public void Create_CustomGrid(int width_count, int height_count, int cell_width, int cell_height)
    {
        this.width_count = width_count;
        this.height_count = height_count;
        this.cell_width = cell_width;
        this.cell_height = cell_height;

        
        //Debug.Log("grid create");
        customgrid = new CustomGrid();
        customgrid.Initialize(width_count, height_count, cell_width, cell_height);
        SceneView.RepaintAll();

    }

    private void Create_TileCube(Vector2Int pos)
    {

        if (pos.x < 0 || pos.y < 0 )
            return;


        //해당 위치에 같은 타일 있으면 return
        //에디터에서 선택한 타일과 같은 타일인지 비교해야함
        //선택된 타일이 뭔지 알고 있어야함
        //어떻게 선택된 정보를 가져오지? prefs로 ?
        //editorwindow에서 보여지는 타일은 image고 SceneView는 큐브라서 타입이아닌 정보를 저장 공유해야
        //editorwindow에서 이미지 클릭하면 정보 콜백함수로 전달하거나 따로 저장해서 읽어와야한다.

        //해당 위치에 이미 있으면 기존거 지우고 새로 만들것

        if (m_tileObj_dic.ContainsKey(pos))
        {
            
            GameObject obj = m_tileObj_dic[pos];
            m_tileObj_dic.Remove(pos);

            GameObject.DestroyImmediate(obj);

        }


        var cube = GameObject.Instantiate(base_tile_prefab);
        //cube.transform.position
        //var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cube.transform.Rotate(0, 0, 180);
        cube.transform.localScale = new Vector3(cell_width, cell_height);
        cube.transform.position = customgrid.Get_CellPos(pos);
        cube.GetComponent<TileObject>().Set_SpriteName(sprites[0].name);

        //씬뷰에서 큐브 터치하면 이동되니까 이동시 원래 자리로 이동하게 하기?
        //아니면 위에 하나 덮어야한

        //map 생성하기 누르면 현재 맵에 있는 오브젝트 싹다 지우기



        base_material.mainTexture = sprites[0].texture;

        cube.GetComponent<MeshRenderer>().material = base_material;

        m_tileObj_dic[pos] = cube;
    }

    public void DrawGUI()
    {
        

        if (customgrid == null)
        {
            return;
        }

        Handles.DrawLines(customgrid.Get_RowLineVec());
        Handles.DrawLines(customgrid.Get_ColumnLineVec());


        Vector2 mousePosition = Event.current.mousePosition;
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
        mousePosition = ray.origin;

        

        switch (Event.current.type)
        {
            case EventType.MouseDown:

                //타일 좌표 받아오고 해당 위치에 타일 그리기.
                //타일 이미지는 오브젝트로 생성해야할듯. 좌표만 받아오고
                

                Create_TileCube(customgrid.Get_Current_Selected_Tile_Pos(mousePosition));

                
                break;
            //드래그도 되게 하고
            case EventType.MouseDrag:

                break;
        }

    }

    public void DestroyObjects()
    {
        foreach(var child in m_tileObj_dic)
        {
            GameObject.DestroyImmediate(child.Value);
        }

        m_tileObj_dic.Clear();
    }

    
}
