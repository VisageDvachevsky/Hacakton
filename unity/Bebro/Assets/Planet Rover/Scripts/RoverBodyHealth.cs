using System;
using UnityEngine;
using UnityEngine.Events;

namespace Rover
{
    public class RoverBodyHealth : MonoBehaviour
    {
        [SerializeField] private DamageTrigger _damageTrigger;
        public event Action OnBroken;

        public bool IsBroken { get; private set; } = false;

        private void OnEnable()
        {
            _damageTrigger.OnDamage.AddListener(TakeDamage);
        }

        private void OnDisable()
        {
            _damageTrigger.OnDamage.RemoveListener(TakeDamage);
        }

        private void TakeDamage()
        {
            if (!IsBroken)
            {
                Debug.Log("Body broken");
                IsBroken = true;
                OnBroken?.Invoke();
            }
        }
    }
}