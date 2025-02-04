using UnityEngine;
using UnityEngine.InputSystem;

namespace Brawlley
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] Rigidbody2D rb;
        GameInputs inputs;

        void Awake()
        {
            inputs = new GameInputs();
            /* 
                Enable the Player action map for testing
                To-Do: Manage the input actions in a more organized way
            */
            inputs.Player.Enable();
        }

        void Start()
        {
            if (rb == null)
                rb = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            Vector2 direction = inputs.Player.Movement.ReadValue<Vector2>();
            rb.linearVelocityX = direction.x * speed;
        }
    }
}
