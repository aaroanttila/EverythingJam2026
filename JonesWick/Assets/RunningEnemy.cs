using Q3Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class RunningEnemy : EnemyAI
{
    public float attackRadius;

    Vector3 LastAttackPos = new Vector3(0,-100, 0);

    protected override void AttackEffect(GameObject AttackTarget, Vector3 AttackPoint)
    {
        Vector3 ToTarget = AttackPoint - transform.position;    

        LastAttackPos = transform.position + ToTarget.normalized;

        Effects.Particle("Particle 1", LastAttackPos, 3, transform);

        Collider[] hits = Physics.OverlapSphere(LastAttackPos, attackRadius);

        foreach (Collider c in hits) {

            if (c.GetComponentInParent<HP>())
            {
                c.GetComponentInParent<HP>().takeDamage(1);
            }
        }

        //some VFX shit lol
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(LastAttackPos, attackRadius);
    }
}
