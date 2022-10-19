using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class MyGridScrollView : ScrollView
{
    private VisualElement[] m_Elements;
    private int row_length;
    //private const float element_height = 50.0f;

    public MyGridScrollView(int row_length)
    {
        this.row_length = row_length;

    }


    public void Add_Elements(Image[] elements)
    {
        int column_length = elements.Length;

        m_Elements = new VisualElement[column_length];

        for(int i = 0; i < column_length; i++)
        {
            m_Elements[i] = new VisualElement();
            m_Elements[i].style.alignItems = Align.FlexStart;
            m_Elements[i].style.flexDirection = FlexDirection.Row;

            //float scale = element_height / elements[i].sprite.texture.height;

            elements[i].style.height = 50;
            elements[i].style.width = 50;

        }

        int index = 0;
        foreach(var child in elements)
        {
            m_Elements[index / row_length].Add(child);
            index++;
        }

        VisualElement child_ve = new VisualElement();
        child_ve.style.alignItems = Align.FlexStart;
        child_ve.style.flexDirection = FlexDirection.Column;

        foreach(var row_ve in m_Elements)
        {
            child_ve.Add(row_ve);
        }

        Add(child_ve);
        
    }

}
