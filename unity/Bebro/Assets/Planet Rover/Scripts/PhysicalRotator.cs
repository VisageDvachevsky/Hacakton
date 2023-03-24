using System;
using UnityEngine;

namespace Rover
{
    public class PhysicalRotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _axis;

        public void SetTargetAngle(float angle)
        {
            transform.localRotation = Quaternion.AngleAxis(angle, _axis);
        }
    }
}