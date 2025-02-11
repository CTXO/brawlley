using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameInputs gameInputs;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;

    void Awake()
    {
        gameInputs = new GameInputs(); // Initialize input system

        // Get references to your movement and jump scripts
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();

        Debug.Log("PlayerController Awake: " + gameObject.activeSelf);

    }

    void OnEnable()
    {
        gameInputs.Player.Enable(); // Enable inputs

        // Subscribe functions to input events
        gameInputs.Player.Movement.performed += playerMovement.OnMovement;
        gameInputs.Player.Movement.canceled += playerMovement.OnMovement; // Ensures movement stops when releasing key

        gameInputs.Player.Jump.started += playerJump.OnJump;
        gameInputs.Player.Jump.canceled += playerJump.OnJump;
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        gameInputs.Player.Movement.performed -= playerMovement.OnMovement;
        gameInputs.Player.Movement.canceled -= playerMovement.OnMovement;

        gameInputs.Player.Jump.started -= playerJump.OnJump;
        gameInputs.Player.Jump.canceled -= playerJump.OnJump;

        gameInputs.Player.Disable(); // Disable inputs
    }
}
