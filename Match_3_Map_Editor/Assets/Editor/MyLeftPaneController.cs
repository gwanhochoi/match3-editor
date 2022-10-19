using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MyLeftPaneController
{
    private TwoPaneSplitView m_twoPaneSplitView;
    private VisualElement m_Portrait_Pane;
    private VisualElement m_TileList_Pane;
    private MyGridScrollView m_GridScrollView;
    private MyPortraitController m_PortraitController;

    //private Image m_portrait_img;
    //private Label m_portrait_label;
    //private Label m_protrait_size;

    public MyLeftPaneController(float flexStartDimension, TwoPaneSplitViewOrientation orientation)
    {
        m_twoPaneSplitView = new TwoPaneSplitView();
        m_twoPaneSplitView.fixedPaneIndex = 0;
        m_twoPaneSplitView.fixedPaneInitialDimension = flexStartDimension;
        m_twoPaneSplitView.orientation = orientation;
        m_GridScrollView = new MyGridScrollView(4);
        m_PortraitController = new MyPortraitController();

        //m_portrait_img = new Image();
        //m_portrait_label = new Label();
        //m_protrait_size = new Label();

        Init();
    }

    public TwoPaneSplitView Get_PaneSplitView()
    {
        return m_twoPaneSplitView;
    }

    private void Init()
    {
        m_Portrait_Pane = new VisualElement();
        m_TileList_Pane = new VisualElement();


        //m_twoPaneSplitView.Add(m_Portrait_Pane);
        m_twoPaneSplitView.Add(m_PortraitController);
        m_twoPaneSplitView.Add(m_TileList_Pane);
        m_twoPaneSplitView.style.minHeight = 100;
        
        //m_Portrait_Pane.style.al




        //m_Portrait_Pane.Add(m_portrait_img);
        //m_Portrait_Pane.Add(m_portrait_label);
        //m_Portrait_Pane.Add(m_protrait_size);

        Sprite[] sprites = Resources.LoadAll<Sprite>("Terrain_Tiles");


        Image[] tile_Array = new Image[sprites.Length];
        for(int i = 0; i < sprites.Length; i++)
        {
            tile_Array[i] = new Image();
            tile_Array[i].sprite = sprites[i];
            tile_Array[i].style.paddingLeft = 2;
            //tile_Array[i].style.paddingRight = 1;
            tile_Array[i].style.paddingTop = 2;
            //tile_Array[i].style.paddingBottom = 1;
            tile_Array[i].RegisterCallback<ClickEvent>(Tile_ClickEvent);
     
            //tile_Array[i].style.height

        }
        m_GridScrollView.Add_Elements(tile_Array);

        m_TileList_Pane.Add(m_GridScrollView);
    }

    private Image selecte_Img;
    private void Tile_ClickEvent(ClickEvent ev)
    {
        var img = (Image)ev.target;
        if (img == null)
            return;

        img.tintColor = new Color(0.3f, 0.4f, 0.7f);
        if (selecte_Img != null)
            selecte_Img.tintColor = new Color(1, 1, 1);
        selecte_Img = img;
        //m_portrait_img.sprite = img.sprite;
        //m_portrait_label.text = img.sprite.name;
        //m_protrait_size.text = img.sprite.texture.width.ToString() + "x" +
        //    img.sprite.texture.height.ToString();
        m_PortraitController.Change_Portrait(img);
    }




}
