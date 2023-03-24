using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NewInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool _Pressed = Input.GetKey("Space");
        float axis = Input.GetAxis("Horizontal");
        print($"Pressed: {_Pressed}");
        print($"Axis: {axis}");
    }
}
