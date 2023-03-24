using UnityEngine;
using UnityEngine.Events;

namespace Rover
{
    public class ArmTrigger : MonoBehaviour
    {
        public UnityEvent OnTriggerEntered;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<ArmTrigger>() && !other.GetComponent<GrabTrigger>())
                OnTriggerEntered?.Invoke();
        }
    }
}