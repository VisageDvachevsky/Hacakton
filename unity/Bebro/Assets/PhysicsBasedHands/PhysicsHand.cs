using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsHand : MonoBehaviour
{
    [SerializeField] private Transform _trackedTransform = null;

    [SerializeField] private float _positionStrength = 20;
    [SerializeField] private float _positionThreshold = 0.005f;
    [SerializeField] private float _maxDistance = 1f;
    [SerializeField] private float _rotationStrength = 30;
    [SerializeField] private float _rotationThreshold = 10f;

    private Rigidbody _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var distance = Vector3.Distance(_trackedTransform.position, _body.position);
        if (distance > _maxDistance || distance < _positionThreshold)
        {
            _body.MovePosition(_trackedTransform.position);
        }
        else
        {
            var vel = (_trackedTransform.position - _body.position).normalized * _positionStrength * distance;
            _body.velocity = vel;
        }

        float angleDistance = Quaternion.Angle(_body.rotation, _trackedTransform.rotation);
        if (angleDistance < _rotationThreshold)
        {
            _body.MoveRotation(_trackedTransform.rotation);
        }
        else
        {
            float kp = (6f * _rotationStrength) * (6f * _rotationStrength) * 0.25f;
            float kd = 4.5f * _rotationStrength;
            Vector3 x;
            float xMag;
            Quaternion q = _trackedTransform.rotation * Quaternion.Inverse(transform.rotation);
            q.ToAngleAxis(out xMag, out x);
            x.Normalize();
            x *= Mathf.Deg2Rad;
            Vector3 pidv = kp * x * xMag - kd * _body.angularVelocity;
            Quaternion rotInertia2World = _body.inertiaTensorRotation * transform.rotation;
            pidv = Quaternion.Inverse(rotInertia2World) * pidv;
            pidv.Scale(_body.inertiaTensor);
            pidv = rotInertia2World * pidv;
            _body.AddTorque(pidv);
        }
    }
}