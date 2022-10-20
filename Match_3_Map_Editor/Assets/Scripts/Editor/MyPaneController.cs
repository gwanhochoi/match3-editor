using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MyPaneController
{
    protected TwoPaneSplitView m_twoPaneSplitView;
    protected VisualElement m_LeftPane;
    protected VisualElement m_RightPane;

    //public float m_flexStartDimension;
    //public TwoPaneSplitViewOrientation m_orientation;

    public MyPaneController() { }

    public MyPaneController(float flexStartDimension, TwoPaneSplitViewOrientation orientation)
    {
        m_twoPaneSplitView = new TwoPaneSplitView();
        m_twoPaneSplitView.fixedPaneIndex = 0;
        m_twoPaneSplitView.fixedPaneInitialDimension = flexStartDimension;
        m_twoPaneSplitView.orientation = orientation;

        m_LeftPane = new VisualElement();
        m_RightPane = new VisualElement();

        m_twoPaneSplitView.Add(m_LeftPane);
        m_twoPaneSplitView.Add(m_RightPane);

        //m_flexStartDimension = flexStartDimension;
        //m_orientation = orientation;

    }

    protected virtual void Init()
    {

    }


    public TwoPaneSplitView Get_PaneSplitView()
    {
        return m_twoPaneSplitView;
    }

}
