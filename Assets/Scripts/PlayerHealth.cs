using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 0f;

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float newHealth)
    {
        health += newHealth;

    }
}
