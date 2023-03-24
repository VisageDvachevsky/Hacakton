using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Rover
{
    public class DamageTrigger : MonoBehaviour
    {
        [SerializeField] private float _damageSpeed;

        public UnityEvent OnDamage;

        private Vector3 _lastPosition;
        private float _currentSpeed;

        private void Start()
        {
            _lastPosition = transform.position;
        }

        private void FixedUpdate()
        {
            _currentSpeed = (transform.position - _lastPosition).magnitude / Time.fixedDeltaTime;
            _lastPosition = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_currentSpeed > _damageSpeed)
            {
                OnDamage?.Invoke();
            }
        }
    }
}