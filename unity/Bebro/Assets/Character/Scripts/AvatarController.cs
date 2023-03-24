using UnityEngine;

[System.Serializable]
public class MapTransform
{
    [SerializeField] private Transform VrTarget;
    [SerializeField] private Transform IKTarget;
    [SerializeField] private Vector3 TrackingPositionOffset;
    [SerializeField] private Quaternion TrackingRotationOffset;

    public void MapVRAvatar()
    {
        IKTarget.position = VrTarget.TransformPoint(TrackingPositionOffset);
        IKTarget.rotation = VrTarget.rotation * TrackingRotationOffset;
    }
}
public class AvatarController : MonoBehaviour
{
    [SerializeField] private MapTransform _head;
    [SerializeField] private MapTransform _leftHand;
    [SerializeField] private MapTransform _rightHand;

    [SerializeField] private float _turnSmoothness;

    [SerializeField] private Transform _IKHead;

    [SerializeField] private Vector3 _headBodyOffset;

    private void LateUpdate()
    {
        transform.position = _IKHead.position + _headBodyOffset;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(_IKHead.forward, Vector3.up).normalized, Time.deltaTime * _turnSmoothness); ;
        _head.MapVRAvatar();
        _leftHand.MapVRAvatar();
        _rightHand.MapVRAvatar();
    }
}
