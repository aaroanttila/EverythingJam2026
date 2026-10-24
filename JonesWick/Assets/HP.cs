using UnityEngine;

public class HP : MonoBehaviour
{
    public int MaxHP;
    public int CurrentHP;
    public bool IsDead;

    private void Start()
    {
        CurrentHP = MaxHP;
    }

    public virtual void Die()
    {
        IsDead = true;
    }

    public virtual void takeDamage(int damage)
    {
        if(IsDead)
            return;

        CurrentHP -= damage;

        if (CurrentHP <= 0) {
             Die();
        }
    }
}
