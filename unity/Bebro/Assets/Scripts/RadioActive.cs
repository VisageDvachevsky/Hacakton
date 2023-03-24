using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioActive : MonoBehaviour
{
    public AudioSource _aud;
    private bool _bebro = false;
    public void _RadioOn()
    {
        print("1");
        if(_bebro == false)
        {
            print("2");
            _aud.volume = 1;
            _bebro = !_bebro;
        }
        else if(_bebro == true)
        {
            _aud.volume = 0;
            _bebro = !_bebro;
        }
    }

}
