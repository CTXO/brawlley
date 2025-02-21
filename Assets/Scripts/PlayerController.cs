using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private GameInputs gameInputs;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerDash playerDash;
    private Vector2 playerDirection;

    void Awake()
    {
        gameInputs = new GameInputs(); // Initialize input system

        // Get references to your movement and jump scripts
        playerJump = GetComponent<PlayerJump>();
        playerDash = GetComponent<PlayerDash>();
        playerMovement = GetComponent<PlayerMovement>();
        Debug.Log("PlayerController Awake: " + gameObject.activeSelf);

    }

    void OnEnable()
    {
        gameInputs.Player.Enable(); // Enable inputs

        // Subscribe functions to input events
        gameInputs.Player.Movement.performed += SetDirection;
        gameInputs.Player.Movement.canceled += SetDirection; // Ensures movement stops when releasing key

        gameInputs.Player.Jump.started += playerJump.OnJump;
        gameInputs.Player.Dash.started += playerDash.OnDash;
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        gameInputs.Player.Movement.performed -= SetDirection;
        gameInputs.Player.Movement.canceled -= SetDirection;

        gameInputs.Player.Jump.started -= playerJump.OnJump;
        gameInputs.Player.Dash.started -= playerDash.OnDash;

        gameInputs.Player.Disable(); // Disable inputs
    }

    void SetDirection(InputAction.CallbackContext context)
    {
        playerDirection = context.ReadValue<Vector2>();
        playerMovement.UpdateDirection(playerDirection.x);
        playerJump.UpdateDirection(Mathf.Min(0, playerDirection.y));
        playerDash.UpdateDirection(playerDirection.normalized);
    }
    
    //Considerar Apagar se não for usar
    public Vector2 GetDirection()
    {
        return playerDirection;
    }
}
