using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TileObject : MonoBehaviour
{
    //private GameObject obj;
    [SerializeField]
    private string sprite_name;

    //public TileObject(string name)
    //{
    //    //this.obj = obj;
    //    this.sprite_name = name;
    //}

    public void Set_SpriteName(string name)
    {
        this.sprite_name = name;
    }

}
