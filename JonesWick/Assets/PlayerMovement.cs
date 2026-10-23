using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;

    public Transform CameraTransform;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forward = Vector3.Cross(CameraTransform.right, Vector3.up);
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        rb.MovePosition(rb.position + (forward * Input.GetAxisRaw("Vertical") * Time.fixedDeltaTime * MovementSpeed));
        rb.MovePosition(rb.position + (right * Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * MovementSpeed));
    }
}
