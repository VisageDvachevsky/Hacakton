using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VRMap
{

    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class VRRig : MonoBehaviour
{
    [Range(0,1)]
    public float turnSmoothness = 1;
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;


    public Transform headConstraint;
    public Transform cameraPosition;
    private Vector3 headBodyOffest;

    // Start is called before the first frame update
    void Start()
    {
        headBodyOffest = transform.position - headConstraint.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = headConstraint.position + headBodyOffest;
        transform.forward = Vector3.Lerp(transform.forward,
         Vector3.ProjectOnPlane(head.vrTarget.transform.forward,Vector3.up).normalized, turnSmoothness);

        head.rigTarget.position = head.vrTarget.TransformPoint(cameraPosition.InverseTransformPoint(headConstraint.position));
        head.rigTarget.rotation = head.vrTarget.rotation * Quaternion.Euler(head.trackingRotationOffset);

        leftHand.Map();
        rightHand.Map();
    }
}
