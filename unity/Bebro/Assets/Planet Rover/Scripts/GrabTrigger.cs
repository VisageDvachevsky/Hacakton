using UnityEngine;

namespace Rover
{
    [RequireComponent(typeof(Rigidbody))]
    public class GrabTrigger : MonoBehaviour
    {
        public Grabable Grabable { get; private set; }

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Attach()
        {
            if (Grabable && !Grabable.GetComponent<FixedJoint>())
            {
                var joint = Grabable.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = _rigidbody;
            }
        }

        public void Detach()
        {
            if (Grabable)
            {
                Destroy(Grabable.GetComponent<FixedJoint>());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Grabable grabable = other.GetComponent<Grabable>();
            if (grabable)
            {
                Grabable = grabable;
                var joint = grabable.gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = _rigidbody;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Grabable>() == Grabable)
            {
                Detach();
                Grabable = null;
            }
        }
    }
}