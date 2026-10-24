using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : HP
{
    public override void Die() {
        SceneManager.LoadScene(1);
    }
}
