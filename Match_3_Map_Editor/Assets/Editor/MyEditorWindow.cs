using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;



public class MyEditorWindow : EditorWindow
{

    private static EditorWindow wnd;

    [MenuItem("MyTool/MapEditor")]
    static void Open_MapEditor()
    {
        wnd = GetWindow<MyEditorWindow>();
        wnd.titleContent = new GUIContent("MyTool");

        wnd.position = new Rect(new Vector2(0, 0),
            new Vector2(Screen.currentResolution.width / 4, Screen.currentResolution.height / 3));
    }

    [MenuItem("MyTool/MapEditor_ShowUtility")]
    static void Open_MapEditor_ShowUtility()
    {
        //Debug.Log("Open MyEditorWindow");

        wnd = ScriptableObject.CreateInstance(typeof(MyEditorWindow)) as MyEditorWindow;

        //wnd = GetWindow<MyEditorWindow>();
        wnd.titleContent = new GUIContent("MyTool");

        wnd.position = new Rect(new Vector2(0, 0),
            new Vector2(Screen.currentResolution.width / 4, Screen.currentResolution.height / 3));

        wnd.ShowUtility();

        //Debug.Log("sreen width = " + Screen.currentResolution.width);
        //Debug.Log("sreen height = " + Screen.currentResolution.height);


        //screen size값이 계속 바뀌어서 현재 해상도에 안맞는다.
        //현재 스크린 윈도우의 사이즈를 구하는 것인지 에디터윈도우 생성시 화면 전체 해상도를 제대로 못읽는것 같다.(지금 생성되는 에디터윈도우의
        //크기를 읽어오나?)
        //screen_width = Screen.width;
        //screen_height = Screen.height;

        //Debug.Log(default_height);


        
        //wnd.minSize = new Vector2(300, 400);
        //wnd.maxSize = new Vector2(1000, 1200);

    }

    private void OnEnable()
    {
        //Debug.Log("onenable");

        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void CreateGUI()
    {

        //var tile_object_guids = AssetDatabase.FindAssets("t:sprite");
        //var allObjects = new List<Sprite>();
        //foreach (var guid in tile_object_guids)
        //{
        //    allObjects.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid)));
        //}

        

        var Parent_Split_View_horizontal = new TwoPaneSplitView(0, Screen.currentResolution.width / 8,
            TwoPaneSplitViewOrientation.Horizontal);


        var left_pane = new VisualElement();
        var right_Pane = new VisualElement();
        
        Parent_Split_View_horizontal.Add(left_pane);
        Parent_Split_View_horizontal.Add(right_Pane);

        var Left_Pane_Controller = new MyLeftPaneController(150, TwoPaneSplitViewOrientation.Vertical);


        left_pane.Add(Left_Pane_Controller.Get_PaneSplitView());


        rootVisualElement.Add(Parent_Split_View_horizontal);

    }


    private void OnSceneGUI(SceneView sceneView)
    {


        Handles.Button(new Vector3(0, 0), Quaternion.identity, 2, 2, Handles.RectangleHandleCap);


        Handles.BeginGUI();
        GUILayout.Button("asdfaddf");
        Handles.EndGUI();

    }

    private void OnDestroy()
    {
        //Debug.Log("closed");
    }
}
