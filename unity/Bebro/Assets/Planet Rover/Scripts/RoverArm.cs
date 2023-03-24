using System.Collections;
using UnityEngine;

namespace Rover
{
    public class RoverArm : MonoBehaviour
    {
        [SerializeField] private ArmTrigger[] _triggers;
        [SerializeField] private GrabTrigger _grabTrigger;
        [SerializeField] private Transform _leftGrab;
        [SerializeField] private Transform _rightGrab;
        [SerializeField] private IKSolver _solver;
        [SerializeField] private float _maxDistance;
        [SerializeField] private PhysicalRotator _axis;
        [SerializeField] private Transform _armPosition;
        [SerializeField] private Transform _target;
        [SerializeField] private float _xSpeed;
        [SerializeField] private float _ySpeed;
        [SerializeField] private float _zSpeed;

        public bool IsActive { get; private set; }

        private float _axisAngle = 0;

        private float _lastAxisAngle = 0;
        private float _lastAxisAngle2 = 0;
        private Vector3 _lastPosition = Vector3.zero;
        private Vector3 _lastPosition2 = Vector3.zero;

        private Vector3 _input = Vector3.zero;
        private float _grabValue;

        private void Start()
        {
            _lastAxisAngle = _axisAngle;
            _lastAxisAngle2 = _axisAngle;
            _lastPosition = _target.localPosition;
            _lastPosition2 = _target.localPosition;

            _target.SetParent(_axis.transform);
        }

        private void OnEnable()
        {
            foreach (var t in _triggers) t.OnTriggerEntered.AddListener(Triggered);
        }

        private void OnDisable()
        {
            foreach (var t in _triggers) t.OnTriggerEntered.RemoveListener(Triggered);
        }

        private void FixedUpdate()
        {
            if (_grabTrigger.Grabable)
            {
                if (_grabValue > _grabTrigger.Grabable.MaxForce)
                {
                    _grabTrigger.Detach();
                    _grabTrigger.Grabable.BreakDown();
                    Debug.Log("Game over");
                }
                else
                {

                    if (_grabValue > _grabTrigger.Grabable.MinForce)
                    {
                        _grabTrigger.Attach();
                    }
                    else
                    {
                        _grabTrigger.Detach();
                    }
                }
            }

            _lastAxisAngle2 = _lastAxisAngle;
            _lastAxisAngle = _axisAngle;
            _lastPosition2 = _lastPosition;
            _lastPosition = _target.localPosition;

            _axisAngle += _input.x * _xSpeed * Time.deltaTime;
            if (_axisAngle > 180) _axisAngle = _axisAngle - 360;
            if (_axisAngle < -180) _axisAngle = 360 + _axisAngle;

            Vector3 offset = new Vector3(0, _input.y * _ySpeed * Time.deltaTime, -_input.z * _zSpeed * Time.deltaTime);
            _target.Translate(offset, Space.Self);

            _axis.SetTargetAngle(_axisAngle);
            _solver.Solve();

            if (_target.localPosition.sqrMagnitude > _maxDistance*_maxDistance)
            {
                _target.localPosition = _target.localPosition.normalized * _maxDistance;
            }
        }

        private void Triggered()
        {
            Debug.Log("triggered");

            _target.localPosition = _lastPosition2;
            _axisAngle = _lastAxisAngle2;
            _axis.SetTargetAngle(_axisAngle);

            _solver.Solve();
        }

        public void SetGrab(float value)
        {
            _grabValue = value;
            _rightGrab.localEulerAngles = new Vector3(0, Mathf.Lerp(0, -20, value), 0);
            _leftGrab.localEulerAngles = new Vector3(0, Mathf.Lerp(0, 20, value), 0);
        }

        public void SetActive(bool state)
        {
            IsActive = state;
        }

        public void Move(float x, float y, float z)
        {
            _input = new Vector3(x, y, z);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(_axis.transform.position, _maxDistance);
        }
    }
}
