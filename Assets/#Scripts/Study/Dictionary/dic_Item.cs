using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dic_Item : MonoBehaviour
{
    private string _name;
    private int str;
    private int dex;

    public dic_Item(string _name, int _str, int _dex)
    {
        this.name = _name;
        this.str = _str;
        this.dex = _dex;
    }

    public void Show()
    {
        Debug.Log(this.name);
        Debug.Log(this.str);
        Debug.Log(this.dex);
    }
}
