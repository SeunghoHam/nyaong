using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefOut : MonoBehaviour
{   
    void Divide_ref(int a,int b, ref int quotient, ref int remainder)
    {
        quotient = a / b;
        remainder = a % b;
    }

    void Divide_out(int a, int b, out int quotient , out int remainder)
    {
        quotient = a / b;
        remainder = a % b;
    }
    private void Start()
    {
        int a = 20, b = 3, c = 0, d = 0;
        //int a= 20 , b = 3, c, d;
        Divide_ref(a, b, ref c, ref d);
        Debug.Log(a + " " + b + " " + c + " " + d);

        Divide_out(a, b, out c, out d);
        Debug.Log(a + " " + b + " " + c + " " + d);

    }
}
