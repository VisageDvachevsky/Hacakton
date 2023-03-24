using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class Minimap : MonoBehaviour
{
    [SerializeField] private Transform _mapCenter;
    [SerializeField] private float _realMapSize;

    private RectTransform _rectTransform;
    private Vector3[] _corners;
    private Vector3 _lbCorner;
    private Vector3 _rtCorner;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _corners = new Vector3[4];
        _rectTransform.GetLocalCorners(_corners);
        _lbCorner = _corners[0];
        _rtCorner = _corners[2];
    }

    public Vector3 GetLocalMinimapPosition(Vector3 worldPosition)
    {
        worldPosition = worldPosition - _mapCenter.position;
        worldPosition.x = Mathf.Clamp(worldPosition.x, -_realMapSize, _realMapSize);
        worldPosition.z = Mathf.Clamp(worldPosition.z, -_realMapSize, _realMapSize);

        return new Vector3(
              Mathf.Lerp(_rectTransform.localPosition.x - _rectTransform.sizeDelta.x / 2, _rectTransform.localPosition.x + _rectTransform.sizeDelta.x / 2, (worldPosition.x + _realMapSize) / (2 * _realMapSize)),
              Mathf.Lerp(_rectTransform.localPosition.y - _rectTransform.sizeDelta.y / 2, _rectTransform.localPosition.y + _rectTransform.sizeDelta.y / 2, (worldPosition.z + _realMapSize) / (2 * _realMapSize)),
              0
            );
    }
}
