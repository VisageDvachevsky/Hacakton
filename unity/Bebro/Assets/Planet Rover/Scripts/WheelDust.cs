using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rover
{
    [RequireComponent(typeof(WheelCollider))]
    public class WheelDust : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _minRpm = 200f;

        private WheelCollider _wheelCollider;

        private void Awake()
        {
            _wheelCollider = GetComponent<WheelCollider>();
        }

        private void Update()
        {
            if (_particleSystem.isPlaying && (_wheelCollider.rpm < _minRpm || !_wheelCollider.isGrounded))
            {
                _particleSystem.Stop();
            }
            else if (!_particleSystem.isPlaying && (_wheelCollider.rpm >= _minRpm && _wheelCollider.isGrounded))
            {
                _particleSystem.Play();
            }
        }
    }
}