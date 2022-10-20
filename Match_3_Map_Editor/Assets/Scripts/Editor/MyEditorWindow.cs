using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;



public class MyEditorWindow : EditorWindow
{

    private SceneViewGUIController sceneViewGUIController;
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

    }

    private void OnEnable()
    {
        //Debug.Log("onenable");

        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
        //Debug.Log("disable");
    }

    
    private void CreateGUI()
    {

        //var tile_object_guids = AssetDatabase.FindAssets("t:sprite");
        //var allObjects = new List<Sprite>();
        //foreach (var guid in tile_object_guids)
        //{
        //    allObjects.Add(AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GUIDToAssetPath(guid)));
        //}

        sceneViewGUIController = new SceneViewGUIController();

        var Parent_Split_View_horizontal = new TwoPaneSplitView(0, Screen.currentResolution.width / 8,
            TwoPaneSplitViewOrientation.Horizontal);


        var left_pane = new VisualElement();
        var right_Pane = new VisualElement();
        
        Parent_Split_View_horizontal.Add(left_pane);
        Parent_Split_View_horizontal.Add(right_Pane);

        var Left_Pane_Controller = new MyLeftPaneController(150, TwoPaneSplitViewOrientation.Vertical);
        left_pane.Add(Left_Pane_Controller.Get_PaneSplitView());

        var Right_Pane_Controller = new MyRightPaneController(200, TwoPaneSplitViewOrientation.Vertical);
        right_Pane.Add(Right_Pane_Controller.Get_PaneSplitView());

        Right_Pane_Controller.Map_Create_Callback = sceneViewGUIController.Create_CustomGrid;

        rootVisualElement.Add(Parent_Split_View_horizontal);

        


    }


    private void OnSceneGUI(SceneView sceneView)
    {

        sceneViewGUIController.DrawGUI();



        //Handles.DrawLine(btn.transform.position + Vector3.up, mousePosition);

        //Debug.Log("mouse x = " + mousePosition.x);
        //Debug.Log("mouse y = " + mousePosition.y);

    }

    private void OnDestroy()
    {
        //Debug.Log("closed");
        sceneViewGUIController.DestroyObjects();

    }
}
