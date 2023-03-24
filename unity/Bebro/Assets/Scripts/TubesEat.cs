using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubesEat : MonoBehaviour
{
    public GameObject _Tubes;
    public MeshFilter _NoEat;
    public AudioSource _eatingSound;
    public Mesh _Eat;
    private int _max = 0;
    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
         
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _max == 0)
        {
            _NoEat.mesh = _Eat;
            _eatingSound.Play();
            _max += 1;
        }
            
    }
}
