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
        playerHealth.Health += 10; // quanto maior a saúde, maior é o empurrão (pouco intuitivo), mas é o como funciona
        playerRb.linearVelocity = direction * playerHealth.Health;
    }

    // Lidar quando o jogador é atingido por um projétil
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Spell spell = collision.GetComponent<Spell>();
        if (spell == null) { return; }

        GetHit(spell.Direction);

        Destroy(collision.gameObject);
    }
    private void Respawn()
    {
        playerHealth.Health = playerHealth.InitialHealth;
        playerRb.linearVelocity = Vector2.zero;
        transform.position = new Vector3(0, 3, 0);
    }
    public void Die()
    {
        playerHealth.Lives--;
        if (playerHealth.Lives <= 0) { gameManager.HandlePlayerElimination(); }
        else { Respawn(); }
    }

}
