using UnityEngine;

namespace Rover
{
    [System.Serializable]
    public struct AxleInfo
    {
        public Wheel leftWheel;
        public Wheel rightWheel;
        public bool motor;
        public bool steering;
        public bool invertSteering;
    }
}