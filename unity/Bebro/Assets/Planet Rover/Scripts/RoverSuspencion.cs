using UnityEngine;

namespace Rover
{
    public class RoverSuspencion : MonoBehaviour
    {
        [SerializeField]
        private WheelCollider _forwardLeft;
        [SerializeField]
        private WheelVisual _forwardLeftVisual;
        [SerializeField]
        private WheelCollider _centerLeft;
        [SerializeField]
        private WheelVisual _centerLeftVisual;
        [SerializeField]
        private WheelCollider _backwardLeft;
        [SerializeField]
        private WheelVisual _backwardLeftVisual;

        [SerializeField]
        private WheelCollider _forwardRight;
        [SerializeField]
        private WheelVisual _forwardRightVisual;
        [SerializeField]
        private WheelCollider _centerRight;
        [SerializeField]
        private WheelVisual _centerRightVisual;
        [SerializeField]
        private WheelCollider _backwardRight;
        [SerializeField]
        private WheelVisual _backwardRightVisual;

        [SerializeField]
        private Transform _leftAxle23;
        [SerializeField]
        private Transform _leftAxle123;
        [SerializeField]
        private Transform _rightAxle23;
        [SerializeField]
        private Transform _rightAxle123;

        [SerializeField]
        [Range(0f, 1f)]
        private float _midAxle23 = 0.5f;


        private Vector3 _forwardLeftPosition;
        private Vector3 _forwardRightPosition;
        private Vector3 _centerLeftPosition;
        private Vector3 _centerRightPosition;
        private Vector3 _backwardLeftPosition;
        private Vector3 _backwardRightPosition;
        
        private Vector3 _ldir23;
        private Vector3 _lmid23;
        private Vector3 _rdir23;
        private Vector3 _rmid23;
        private Vector3 _ldir123;
        private Vector3 _rdir123;

        private void Update()
        {
            GetLocalPoses();
            CalculateWheelRotations();
            ApplyVisibility();
        }

        private void CalculateWheelRotations()
        {
            _ldir23 = (_centerLeftPosition - _forwardLeftPosition).normalized;
            _lmid23 = Vector3.Lerp(_forwardLeftPosition, _centerLeftPosition, _midAxle23);
            _ldir123 = (_lmid23 - _backwardLeftPosition).normalized;

            _rdir23 = (_centerRightPosition - _forwardRightPosition).normalized;
            _rmid23 = Vector3.Lerp(_forwardRightPosition, _centerRightPosition, _midAxle23);
            _rdir123 = (_rmid23 - _backwardRightPosition).normalized;
        }

        private void GetLocalPoses()
        {
            _forwardLeft.GetWorldPose(out _forwardLeftPosition, out _);
            _forwardLeftPosition = transform.InverseTransformPoint(_forwardLeftPosition);
            _forwardRight.GetWorldPose(out _forwardRightPosition, out _);
            _forwardRightPosition = transform.InverseTransformPoint(_forwardRightPosition);
            _centerLeft.GetWorldPose(out _centerLeftPosition, out _);
            _centerLeftPosition = transform.InverseTransformPoint(_centerLeftPosition);
            _centerRight.GetWorldPose(out _centerRightPosition, out _);
            _centerRightPosition = transform.InverseTransformPoint(_centerRightPosition);
            _backwardLeft.GetWorldPose(out _backwardLeftPosition, out _);
            _backwardLeftPosition = transform.InverseTransformPoint(_backwardLeftPosition);
            _backwardRight.GetWorldPose(out _backwardRightPosition, out _);
            _backwardRightPosition = transform.InverseTransformPoint(_backwardRightPosition);
        }

        private void ApplyVisibility()
        {
            _leftAxle123.localRotation = Quaternion.LookRotation(_ldir123);
            _leftAxle23.localRotation = Quaternion.LookRotation(-_ldir23);

            _rightAxle123.localRotation = Quaternion.LookRotation(_rdir123);
            _rightAxle23.localRotation = Quaternion.LookRotation(-_rdir23);

            _forwardLeftVisual.SetValues(_forwardLeft.rpm, _forwardLeft.steerAngle);
            _forwardRightVisual.SetValues(_forwardRight.rpm, _forwardRight.steerAngle);
            _centerLeftVisual.SetValues(_centerLeft.rpm, _centerLeft.steerAngle);
            _centerRightVisual.SetValues(_centerRight.rpm, _centerRight.steerAngle);
            _backwardLeftVisual.SetValues(_backwardLeft.rpm, _backwardLeft.steerAngle);
            _backwardRightVisual.SetValues(_backwardRight.rpm, _backwardRight.steerAngle);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.TransformPoint(_lmid23), 0.1f);
            Gizmos.DrawSphere(transform.TransformPoint(_rmid23), 0.1f);
        }
    }
}