using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] float initialHealth;

    private void Start()
    {
        health = initialHealth;
    }
    public float Health { get => health; set => health = value; }
    public float InitialHealth { get => initialHealth; }
}
