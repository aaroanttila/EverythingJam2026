using Q3Movement;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public enum AIState { 
        Inactive,
        Active,
        Stunned,
        Dead
    }

    AIState aiState = AIState.Inactive;

    public AIConfig config;

    public GameObject Target;    

    public LayerMask TargetingLayerMask;

    public Rigidbody rb;

    Timer AttackTimer = new Timer(0);
    Timer WaitTimer = new Timer(0);

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Target == null)
            Target = FindFirstObjectByType<Q3PlayerController>().gameObject;

        if (!WaitTimer.completed()) {
            return;
        }
        WaitTimer = new Timer(0);
        WaitTimer.restartTimer();

        switch (aiState) {
            case AIState.Inactive:
                if(CheckForTarget())
                    aiState = AIState.Active;
                break;

            case AIState.Active:

                float DistToPlayer = Vector3.Distance(transform.position, Target.transform.position);

                if (DistToPlayer > config.AttackRange)
                {
                    //move to player
                    Vector3 Dir = Target.transform.position - transform.position;
                    Dir = Vector3.ProjectOnPlane(Dir, Vector3.up);
                    Dir.Normalize();

                    rb.MovePosition(transform.position + (Dir * config.movementSpeed * Time.fixedDeltaTime));


                }
                else if (DistToPlayer < config.MoveawayRange)
                {
                    //move away from player
                    Vector3 Dir = transform.position - Target.transform.position ;
                    Dir = Vector3.ProjectOnPlane(Dir, Vector3.up);
                    Dir.Normalize();

                    rb.MovePosition(transform.position + (Dir * config.movementSpeed * Time.fixedDeltaTime));

                }
                else {
                    //try to attack
                    if (AttackTimer.completed())
                    {
                        AttackTimer = new Timer(1 / config.AttacksPerSecond);
                        AttackTimer.restartTimer();

                        SetWaitTimer(config.AttackWaitTime);

                        Attack(Target);


                    }
                }

                break;

            case AIState.Dead:
                
                break;
        }        
    }

    protected virtual void Attack(GameObject AttackTarget)
    {
        StartCoroutine(AttackCoroutine(AttackTarget));
    }

    protected virtual IEnumerator AttackCoroutine(GameObject AttackTarget)
    {
        yield return new WaitForSeconds(config.AttackWindupTime);

        AttackEffect(AttackTarget);        
    }

    protected virtual void AttackEffect(GameObject AttackTarget) {
        Debug.Log("Attack " + AttackTarget.name);
    }

    bool CheckForTarget() {
        RaycastHit hit;

        if (Physics.Linecast(transform.position, Target.transform.position, out hit, TargetingLayerMask)) {
            if (hit.collider.gameObject.CompareTag("PlayerBody")) 
                return true;                
        }

        return false;
    }


    void SetWaitTimer(float newWaitDuration) {
        Timer NewWaitTimer = new Timer(newWaitDuration);
        NewWaitTimer.restartTimer();
        WaitTimer = Timer.Longer(WaitTimer, NewWaitTimer);
    }


    [System.Serializable]
    public class AIConfig {
        public float movementSpeed;
        public float AttackRange;
        public float MoveawayRange;
        public float AttacksPerSecond;
        public float AttackWindupTime;
        public float AttackWaitTime;
    }
}
