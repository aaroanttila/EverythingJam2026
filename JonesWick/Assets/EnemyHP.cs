using UnityEngine;

public class EnemyHP : HP
{
    public override void Die()
    {
        Destroy(gameObject);
    }
}
