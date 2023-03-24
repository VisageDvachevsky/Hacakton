using UnityEngine;

namespace Rover
{
    [RequireComponent(typeof(Rigidbody))]
    public class CenterOfMass : MonoBehaviour
    {
        [SerializeField] private Transform _center;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.centerOfMass = _rigidbody.transform.InverseTransformPoint(_center.position);
        }
    }
}