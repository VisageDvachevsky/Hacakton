using UnityEngine;

namespace Rover
{
    public class IKSolver : MonoBehaviour
    {
        [SerializeField] private Transform _effector;
        [SerializeField] private PhysicalRotator _pivot;
        [SerializeField] private PhysicalRotator _upper;
        [SerializeField] private PhysicalRotator _lower;
        [SerializeField] private Transform _target;

        private float _upperLength;
        private float _lowerLength;
        private Vector3 _effectorTarget;

        void Reset()
        {
            _pivot = GetComponent<PhysicalRotator>();
            try
            {
                _upper = _pivot.transform.GetChild(0).GetComponent<PhysicalRotator>();
                _lower = _upper.transform.GetChild(0).GetComponent<PhysicalRotator>();
                _effector = _lower.transform.GetChild(0);
            }
            catch (UnityException)
            {
                Debug.Log("Could not find required transforms, please assign manually.");
            }
        }

        private void Awake()
        {
            _upperLength = (_lower.transform.position - _upper.transform.position).magnitude;
            _lowerLength = (_effector.position - _lower.transform.position).magnitude;
        }

        public void Solve()
        {
            _effectorTarget = _target.position;

            var upperToTarget = (_effectorTarget - _upper.transform.position);
            var a = _upperLength;
            var b = _lowerLength;
            var c = upperToTarget.magnitude;

            var B = Mathf.Acos((c * c + a * a - b * b) / (2 * c * a)) * Mathf.Rad2Deg;
            var C = Mathf.Acos((a * a + b * b - c * c) / (2 * a * b)) * Mathf.Rad2Deg;
            var phi = Mathf.Atan2(_effectorTarget.y - _upper.transform.position.y, Vector2.Distance(new Vector2(_effectorTarget.x, _effectorTarget.z), new Vector2(_upper.transform.position.x, _upper.transform.position.z))) * Mathf.Rad2Deg;

            if (!float.IsNaN(C))
            {
                _upper.SetTargetAngle(B + phi);
                _lower.SetTargetAngle(C - 180);
            }
        }
    }
}