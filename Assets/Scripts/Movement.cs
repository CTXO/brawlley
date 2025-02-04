using UnityEngine;
using UnityEngine.InputSystem;

namespace Brawlley
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 5f;
        [SerializeField] float jumpForce = 5f;
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
            inputs.Player.Jump.performed += Jump_performed; // Subscribe to the Jump action
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
            rb.linearVelocityX = direction.x * moveSpeed;
        }

        void Jump_performed(InputAction.CallbackContext context) =>
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
