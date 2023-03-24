using UnityEngine;
using UnityEngine.Events;

public class Btn : MonoBehaviour
{
    [SerializeField] private float _yPressed = -0.0525f;
    [SerializeField] private float _yReleased = -0.0397f;

    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRealese;

    private GameObject presser;
    private bool isPressed;


    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(button.transform.localPosition.x, _yPressed, button.transform.localPosition.z)
;
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == presser)
        {
            presser = null;
            button.transform.localPosition = new Vector3(button.transform.localPosition.x, _yReleased, button.transform.localPosition.z);
            onRealese.Invoke();
            isPressed = false;
        }
    }
}
