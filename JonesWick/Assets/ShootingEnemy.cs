using Q3Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingEnemy : EnemyAI
{
    public float ProjectileRadius;
    public float ProjectileSpeed;

    public LayerMask HitMask;

    Vector3 LastAttackPos = new Vector3(0, -100, 0);

    protected override void AttackEffect(GameObject AttackTarget, Vector3 AttackPoint)
    {
        Vector3 ToTarget = AttackTarget.transform.position - transform.position;

        StartCoroutine(projectile(transform.position, ToTarget));
    }

    IEnumerator projectile(Vector3 StartPos, Vector3 Dir) { 
        Vector3 Pos = StartPos;


        bool hitSomething = false;
        while (!hitSomething) {

            yield return null;

            Pos += Dir.normalized * ProjectileSpeed * Time.deltaTime;


            Effects.Particle("Particle 1", Pos, 1, transform);

            Debug.DrawLine(Pos, transform.position + Vector3.up);   
            LastAttackPos = Pos;

            Collider[] hits = Physics.OverlapSphere(Pos, ProjectileRadius, HitMask);

            //some VFX shit lol

            foreach (Collider c in hits)
            {
                if (c.GetComponentInParent<HP>())
                {
                    c.GetComponentInParent<HP>().takeDamage(1);
                }

                hitSomething = true;    
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(LastAttackPos, ProjectileRadius);
    }
}
