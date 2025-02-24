using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] float initialHealth;
    private float health;

    private void Start()
    {
        health = initialHealth;
    }
    public float Health { get => health; set => health = value; }
    public float InitialHealth { get => initialHealth; }

    public int Lives { get => playerLives; set => playerLives = value; }
}
