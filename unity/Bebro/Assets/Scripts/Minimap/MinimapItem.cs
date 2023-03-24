using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MinimapItem : MonoBehaviour
{
    [SerializeField] private Minimap _minimap;
    [SerializeField] private Transform _trackingObject;
    [SerializeField] private bool _handleRotation;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        _rectTransform.localPosition = _minimap.GetLocalMinimapPosition(_trackingObject.position);
        if (_handleRotation)
        {
            _rectTransform.localRotation = Quaternion.Euler(0, 0, -_trackingObject.eulerAngles.y);
        }
    }
}
