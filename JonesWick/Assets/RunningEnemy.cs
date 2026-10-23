using Q3Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunningEnemy : EnemyAI
{
    public float attackRadius;

    Vector3 LastAttackPos = new Vector3(0,-100, 0);

    protected override void AttackEffect(GameObject AttackTarget)
    {
        Vector3 ToTarget = AttackTarget.transform.position - transform.position;    

        LastAttackPos = transform.position + ToTarget.normalized;


        Collider[] hits = Physics.OverlapSphere(LastAttackPos, attackRadius);

        foreach (Collider c in hits) {
            if (c.GetComponentInParent<Q3PlayerController>()) {
                SceneManager.LoadScene(1);
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
