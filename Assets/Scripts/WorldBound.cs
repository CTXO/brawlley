using UnityEngine;

public class WorldBound : MonoBehaviour
{
    [SerializeField] float horizontalBound = 60f;
    [SerializeField] float verticalBound = 40f;

    private BoxCollider2D boundsDetector;

    void Start()
    {
        boundsDetector = GetComponent<BoxCollider2D>();
        UpdateBoundsSize();
    }

    // Update só existe para poder mudar o tamanho do mundo enquanto tá dentro do jogo. Remover quando não for mais testar
    //void Update()
    //{
    //    UpdateBoundsSize();
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Objeto saiu do limite: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHurt player = collision.gameObject.GetComponent<PlayerHurt>();
            player.Respawn();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }

    void UpdateBoundsSize()
    {
        boundsDetector.size = new Vector2(horizontalBound, verticalBound);
    }
}
