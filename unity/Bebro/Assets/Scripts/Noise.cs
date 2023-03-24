using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Noise : MonoBehaviour
{
    public RawImage _noise;
    private void Update()
    {
        var _alpha = GetComponent<Material>().color.a;
        _alpha = 1f * Time.deltaTime;
    }
}
