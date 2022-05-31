using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfNumbers : MonoBehaviour
{ 
    enum myEnum { a,b,c};

    
    // Start is called before the first frame update
    void Start()
    {
        var myDictionary = new Dictionary<myEnum, object>();
        myDictionary.Add(myEnum.a, new object());
    }

    // Update is called once per frame
    void Update()
    {

    }
}

