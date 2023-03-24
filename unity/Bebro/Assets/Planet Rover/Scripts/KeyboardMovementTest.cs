using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rover
{
    [RequireComponent(typeof(Rover))]
    public class KeyboardMovementTest : MonoBehaviour
    {
        private Rover _rover;
        private float _acceleration;
        private float _steer;

        private float _a;

        private void Awake()
        {
            _rover = GetComponent<Rover>();
        }

        private void Update()
        {
            _acceleration = Input.GetAxis("Vertical");
            _steer = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.Z)) _rover.TurnOn();
            if (Input.GetKeyDown(KeyCode.X)) _rover.TurnOff();

            if (Input.GetKey(KeyCode.F))
            {
                _rover.MoveArm(-1, 0, 0);
            }
            else if (Input.GetKey(KeyCode.H))
            {
                _rover.MoveArm(1, 0, 0);
            }
            else if (Input.GetKey(KeyCode.T))
            {
                _rover.MoveArm(0, 0, 1);
            }
            else if (Input.GetKey(KeyCode.G))
            {
                _rover.MoveArm(0, 0, -1);
            }
            else if (Input.GetKey(KeyCode.U))
            {
                _rover.MoveArm(0, 1, 0);
            }
            else if (Input.GetKey(KeyCode.J))
            {
                _rover.MoveArm(0, -1, 0);
            }
            else
            {
                _rover.MoveArm(0, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                _rover.OpenGreenBox();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                _rover.OpenYellowBox();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                _rover.OpenBlueBox();
            }

            if (Input.GetKey(KeyCode.V))
            {
                _a = Mathf.MoveTowards(_a, 0, Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.B))
            {
                _a = Mathf.MoveTowards(_a, 1, Time.deltaTime);
            }

            _rover.SetArmGrab(_a);
        }

        private void FixedUpdate()
        {
            _rover.Move(_acceleration, _steer);
        }
    }
}