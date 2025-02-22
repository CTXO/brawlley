using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    #region Components
    [Header("Components")]
    GameInputs gameInputs;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerJump playerJump;
    [SerializeField] PlayerDash playerDash;
    #endregion

    #region Data
    Vector2 playerDirection;
    #endregion

    #region Properties
    public GameInputs GameInputs => gameInputs;
    public Vector2 PlayerDirection => playerDirection;
    #endregion

    #region MonoBehaviour Lifecycle Methods
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
        gameInputs.Attack.Enable();

        // Subscribe functions to input events
        gameInputs.Player.Movement.performed += SetDirection;
        gameInputs.Player.Movement.canceled += SetDirection; // Ensures movement stops when releasing key
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        gameInputs.Player.Movement.performed -= SetDirection;
        gameInputs.Player.Movement.canceled -= SetDirection;

        gameInputs.Player.Disable(); // Disable inputs
        gameInputs.Attack.Disable();
    }
    #endregion

    #region Player Controller Methods
    void SetDirection(InputAction.CallbackContext context)
    {
        playerDirection = context.ReadValue<Vector2>();
        if (playerMovement != null)
            playerMovement.UpdateDirection(playerDirection.x);
        if (playerJump != null)
            playerJump.UpdateDirection(Mathf.Min(0, playerDirection.y));
        if (playerDash != null)
            playerDash.UpdateDirection(playerDirection.normalized);
    }
    
    //Considerar Apagar se n�o for usar
    public Vector2 GetDirection()
    {
        return playerDirection;
    }
    #endregion
}
