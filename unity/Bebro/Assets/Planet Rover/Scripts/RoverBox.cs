using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rover
{
    public class RoverBox : MonoBehaviour
    {
        [SerializeField] private Transform _topPlate;
        [SerializeField] private float _closedPos;
        [SerializeField] private float _openedPos;
        [SerializeField] private SampleKind _sampleKind;

        public enum SampleKind {
            Green,
            Yellow,
            Red
        }

        private bool _isClosed = true;

        public BoxState BoxState { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (BoxState != BoxState.Opened) return;

            var grabable = other.GetComponent<Grabable>();
            if (grabable)
            {
                _isClosed = true;
                if (_sampleKind == grabable.SampleKind)
                {
                    BoxState = BoxState.Filled;

                    switch (_sampleKind)
                    {
                        case SampleKind.Green:
                            Tasks.SetGreenCollected();
                            break;
                        case SampleKind.Yellow:
                            Tasks.SetYellowCollected();
                            break;
                        case SampleKind.Red:
                            Tasks.SetRedCollected();
                            break;
                    }
                }
                else
                {
                    BoxState = BoxState.FilledInvalid;
                    Debug.Log("Invalid fill");
                }
                StartCoroutine(DisableGrabable(grabable));
            } 
        }

        private IEnumerator DisableGrabable(Grabable grabable)
        {
            yield return new WaitForSeconds(1f);
            grabable.gameObject.SetActive(false);
        }

        private void Start()
        {
            _topPlate.localPosition = new Vector3(_closedPos, _topPlate.localPosition.y, _topPlate.localPosition.z);
            BoxState = BoxState.Closed;
        }

        private void Update()
        {
            _topPlate.localPosition = new Vector3(Mathf.MoveTowards(_topPlate.localPosition.x, _isClosed ? _closedPos : _openedPos, Time.deltaTime), _topPlate.localPosition.y, _topPlate.localPosition.z);
        }

        public void Open()
        {
            if (BoxState == BoxState.Closed)
            {
                _isClosed = false;
                BoxState = BoxState.Opened;
            }
        }

        public void Close()
        {
            if (BoxState == BoxState.Opened)
            {
                _isClosed = true;
                BoxState = BoxState.Closed;
            }
        }
    }
}