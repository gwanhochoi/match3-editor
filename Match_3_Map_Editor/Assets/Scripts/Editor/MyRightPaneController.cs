using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class MyRightPaneController : MyPaneController
{
    //private TwoPaneSplitView m_MapSplitView;

    private Label m_widthCount_Label;
    private Label m_heightCount_Label;

    private IntegerField m_widthCount_IF;
    private IntegerField m_heightCount_IF;

    private Label m_cellWidth_Label;
    private Label m_cellHeight_Label;

    private IntegerField m_cellWidth_IF;
    private IntegerField m_cellHeight_IF;



    public delegate void MapCreateDelegate(int width_count, int height_count, int cell_width, int cell_height);
    public MapCreateDelegate Map_Create_Callback;

    
    public  MyRightPaneController(float flexStartDimension, TwoPaneSplitViewOrientation orientation)
        : base(flexStartDimension, orientation)
    {
        
        Init();
        
    }

    protected override void Init()
    {

        //m_LeftPane.style.alignItems = Align.Center;
        var cell_count_field = new VisualElement();
        var cell_size_field = new VisualElement();
        
        //cell_count_field.style.alignItems = Align.Stretch;
        cell_count_field.style.flexDirection = FlexDirection.Row;
        //cell_count_field.style.justifyContent = Justify.Center;
        cell_count_field.style.backgroundColor = new Color(0.3f, 0.3f, 0.3f);

        cell_size_field.style.flexDirection = FlexDirection.Row;
        cell_size_field.style.backgroundColor = new Color(0.3f, 0.3f, 0.3f);

        m_widthCount_Label = new Label("width_count");
        m_heightCount_Label = new Label("height_count");

        m_widthCount_Label.style.width = 80;
        m_heightCount_Label.style.width = 80;

        m_widthCount_IF = new IntegerField(2);
        m_heightCount_IF = new IntegerField(2);

        m_widthCount_IF.style.width = 50;
        m_heightCount_IF.style.width = 50;

        m_cellWidth_Label = new Label("cell_width");
        m_cellHeight_Label = new Label("cell_height");

        m_cellWidth_Label.style.width = 80;
        m_cellHeight_Label.style.width = 80;

        m_cellWidth_IF = new IntegerField();
        m_cellHeight_IF = new IntegerField();

        m_cellWidth_IF.style.width = 50;
        m_cellHeight_IF.style.width = 50;


        cell_count_field.Add(m_widthCount_Label);
        cell_count_field.Add(m_widthCount_IF);
        cell_count_field.Add(m_heightCount_Label);
        cell_count_field.Add(m_heightCount_IF);

        cell_size_field.Add(m_cellWidth_Label);
        cell_size_field.Add(m_cellWidth_IF);
        cell_size_field.Add(m_cellHeight_Label);
        cell_size_field.Add(m_cellHeight_IF);



        var create_button = new Button();
        create_button.text = "create button";
        create_button.clicked += Create_Map_Button;

        m_LeftPane.Add(cell_count_field);
        m_LeftPane.Add(cell_size_field);
        m_LeftPane.Add(create_button);

    }

    void Create_Map_Button()
    {
        //Debug.Log("width_value = " + m_widthCount_IF.value);
        //Debug.Log("height_value = " + m_heightCount_IF.value);
        if (m_widthCount_IF.value < 1 || m_heightCount_IF.value < 1
            || m_cellWidth_IF.value < 1 || m_cellHeight_IF.value < 1)
            return;


        Map_Create_Callback(m_widthCount_IF.value, m_heightCount_IF.value,
            m_cellWidth_IF.value, m_cellHeight_IF.value);
    }
}
