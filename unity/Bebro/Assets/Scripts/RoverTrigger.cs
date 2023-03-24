using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoverTrigger : MonoBehaviour
{
    [SerializeField] private Rover.Rover _rover;
    [SerializeField] private float _minDistance;

    public UnityEvent OnZoneEntered;
    public UnityEvent OnStoppedInZone;
    public UnityEvent OnZoneExited;
    public bool IsRoverTriggered { get; private set; } = false;
    private bool _isStopped = false;

    private void Update()
    {
        bool newIsTriggered = Vector3.Distance(_rover.transform.position, transform.position) < _minDistance;

        if (IsRoverTriggered && !_isStopped && _rover.GetTelemetry().Speed < 1)
        {
            _isStopped = true;
            OnStoppedInZone?.Invoke();
        }

        if (IsRoverTriggered != newIsTriggered)
        {
            if (newIsTriggered)
            {
                Debug.Log("entered zone");
                OnZoneEntered?.Invoke();
            }
            else
            {
                Debug.Log("exited zone");
                OnZoneExited?.Invoke();
                _isStopped = false;
            }

            IsRoverTriggered = newIsTriggered;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _minDistance);
    }
}
