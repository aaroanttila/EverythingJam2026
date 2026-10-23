using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeViewModelFollowCameraWTF : MonoBehaviour
{
    public Rigidbody rb;
    public Transform CameraTransform;

    [Range(1,10)]public float PositionSmoothing = 1;
    [Range(1,10)]public float RotationSmoothing = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 TargetPositon = CameraTransform.position;

        rb.MovePosition(Vector3.Lerp(transform.position, TargetPositon, 1 / PositionSmoothing));

        Quaternion TargetRotation = CameraTransform.rotation;

        rb.MoveRotation(Quaternion.Lerp(transform.rotation, TargetRotation, 1 / RotationSmoothing));
    }
}
