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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GetHit(Vector2 direction)
    {
        float forceMultiplier = playerHealth.Health;
        playerRb.AddForce(direction * forceMultiplier, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entrou");
        playerHealth.Health -= 10;
        GetComponent<PlayerHurt>().GetHit((new Vector2(1,1)) * playerHealth.Health/50);
    }
    private void Respawn()
    {
        playerHealth.Health = playerHealth.InitialHealth;
        transform.position = new Vector3(0, 3, 0);
        playerRb.linearVelocity = Vector2.zero;
    }
    public void Die()
    {
        playerHealth.Lives--;

        if (playerHealth.Lives <= 0)
        {
            gameManager.HandlePlayerElimination();
        }
        else
        {
            Respawn();
        }

    }

}
