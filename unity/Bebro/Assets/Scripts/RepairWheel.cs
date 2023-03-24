using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rover;

public class RepairWheel : MonoBehaviour
{
    [SerializeField] private Rover.Rover _rov;
    [SerializeField] private Wheel _wheel;
    // Start is called before the first frame update
    public void Start()
    {
        _wheel = GetComponent<Wheel>(); 
    }
    private void Update()
    {
        
        if (!_wheel.IsBroken)
        {
            Debug.Log($"{name} broken");
        }
    }
}
