using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MyPortraitController : VisualElement
{
    private Image m_portrait_img;
    private Label m_portrait_label;
    private Label m_protrait_size;


    public MyPortraitController()
    {
        m_portrait_img = new Image();
        m_portrait_label = new Label();
        m_protrait_size = new Label();

        Add(m_portrait_img);
        Add(m_portrait_label);
        Add(m_protrait_size);

        style.alignItems = Align.Center;
    }


    public void Change_Portrait(Image img)
    {
        m_portrait_img.sprite = img.sprite;
        m_portrait_label.text = img.sprite.name;
        m_protrait_size.text = img.sprite.texture.width.ToString() + "x" +
            img.sprite.texture.height.ToString();
    }

}
