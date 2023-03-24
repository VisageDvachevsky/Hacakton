using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Rover
{
    [RequireComponent(typeof(WheelCollider))]
    public class Wheel : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _torqueCurve;
        [SerializeField] private DamageTrigger _damageTrigger;
        [SerializeField] private float _brakeForce;
        [SerializeField] private float _steeringAngle;
        [SerializeField] private float _maxRpm;
        [SerializeField] private int _n;
        [SerializeField] private bool _isBroken;

        public delegate void WheelEvent(int n);
        public event WheelEvent OnBroken;

        private WheelCollider _wheelCollider;
        private float _torque;

        public bool IsBroken => _isBroken;

        private void Awake()
        {
            _wheelCollider = GetComponent<WheelCollider>();
        }

        private void OnEnable()
        {
            _damageTrigger.OnDamage.AddListener(TakeDamage);
        }

        private void OnDisable()
        {
            _damageTrigger.OnDamage.RemoveListener(TakeDamage);
        }

        private void Update()
        {
            _wheelCollider.motorTorque = _torqueCurve.Evaluate(_wheelCollider.rpm) * _torque;
        }


        public bool Repair()
        {
            if (_isBroken)
            {
                _isBroken = false;
                return true;
            }

            return false;
        }

        public void SetTorque(float torque)
        {
            if (torque == 0 || IsBroken)
            {
                _wheelCollider.brakeTorque = _brakeForce;
            } else
            {
                _wheelCollider.brakeTorque = 0;
            }

            _torque = torque;
        }

        public void SetSteering(float steering)
        {
            if (!IsBroken) _wheelCollider.steerAngle = steering * _steeringAngle;
        }
        private void TakeDamage()
        {
            if (!IsBroken) {
                _isBroken = true;
                OnBroken?.Invoke(_n);
                Debug.Log($"{name} broken");
            }
        }
    }
}