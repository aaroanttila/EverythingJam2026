using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStuff : MonoBehaviour
{

    public LayerMask GunHitMask;
    public Transform GunShootPoint;

    public Transform GunShootPointUIElement;

    public Vector3 ShootKnockback;

    public Rigidbody rb;

    Vector3 RayHitPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Physics.Raycast(GunShootPoint.position, GunShootPoint.forward , out hit, 1000, GunHitMask);

        if (hit.collider != null)
        {
            RayHitPoint = hit.point;
            Debug.DrawLine(GunShootPoint.position, RayHitPoint, Color.magenta);
        }
        else {
            RayHitPoint = GunShootPoint.position + (GunShootPoint.forward * 1000);
            Debug.DrawLine(GunShootPoint.position, RayHitPoint, Color.blue);
        }

        GunShootPointUIElement.position = Camera.main.WorldToScreenPoint(RayHitPoint);

        if (Input.GetMouseButtonDown(0)) {
            rb.AddForceAtPosition(transform.rotation * ShootKnockback, GunShootPoint.position, ForceMode.Impulse);
        }
    }
}
