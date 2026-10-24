using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStuff : MonoBehaviour
{

    public LayerMask GunHitMask;
    public Transform GunShootPoint;

    public Transform GunShootPointUIElement;

    public Vector3 ShootKnockback;
    public float ShootKnockbackToOther;

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

            if (hit.collider != null) {
                HP hp = hit.collider.gameObject.GetComponentInParent<HP>();
                
                if (hp != null) {
                    hp.takeDamage(1);
                }

                Rigidbody otherRb = hit.collider.gameObject.GetComponentInParent<Rigidbody>();

                if (otherRb != null)
                {
                    otherRb.AddForceAtPosition(GunShootPoint.forward * ShootKnockbackToOther, hit.point, ForceMode.Impulse);
                }
            }
        }
    }
}
