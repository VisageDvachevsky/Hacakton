using UnityEngine;


namespace RadTech.VRSchool
{
    [RequireComponent(typeof(Rigidbody))]
    internal sealed class CustomGravity : MonoBehaviour
    {
        [SerializeField] private float _gravity;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity += Vector3.up * _gravity * Time.fixedDeltaTime;
        }
    }
}