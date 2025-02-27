using Brawlley.Attacks;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Rigidbody2D playerRb;
    private GameManager gameManager;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<PlayerHealth>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void GetHit(Vector2 direction)
    {
        playerHealth.Damage += 10;

        gameManager.HandlePlayerDamage(this.gameObject, playerHealth.Damage);

        playerRb.linearVelocity = direction * playerHealth.Damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spell spell = collision.GetComponent<Spell>();
        if (spell == null) { return; }

        GetHit(spell.direction);

        Destroy(collision.gameObject);
    }

    private void Respawn()
    {
        playerHealth.ResetHealth();
        playerRb.linearVelocity = Vector2.zero;
        transform.position = new Vector3(0, 3, 0);
    }

    public void Die()
    {
        playerHealth.Lives--;
        gameManager.HandlePlayerDeath(this.gameObject, playerHealth.Lives);

        if (playerHealth.Lives > 0) { Respawn(); }
    }
}
