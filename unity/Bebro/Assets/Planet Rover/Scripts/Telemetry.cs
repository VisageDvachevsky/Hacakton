using UnityEngine;

namespace Rover
{
    public struct Telemetry
    {
        public Vector3 Position;
        public float Direction;
        public float HorizontalAngle;
        public float Speed;
        public float BatteryPercents;

        public int Health;
        public bool BodyBroken;
        public bool LFBroken;
        public bool RFBroken;
        public bool LCBroken;
        public bool RCBroken;
        public bool LBBroken;
        public bool RBBroken;

        public BoxState greenBoxState;
        public BoxState yellowBoxState;
        public BoxState blueBoxState;
    }
}