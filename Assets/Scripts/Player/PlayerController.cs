using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    #region Components
    [Header("Components")]
    GameInputs gameInputs;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerDash playerDash;
    private PlayerParry playerParry;
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
        playerParry = GetComponent<PlayerParry>();

    }

    void OnEnable()
    {
        gameInputs.Player.Enable(); // Enable inputs
        gameInputs.Skill.Enable();

        // Subscribe functions to input events
        gameInputs.Player.Movement.performed += SetDirection;
        gameInputs.Player.Movement.canceled += SetDirection; // Ensures movement stops when releasing key

        gameInputs.Player.Jump.started += playerJump.OnJump;
        gameInputs.Player.Dash.started += playerDash.OnDash;

        gameInputs.Skill.Spell.started += OnAiming;
        gameInputs.Skill.Spell.canceled += OnStopAiming;

        gameInputs.Skill.Parry.started += playerParry.OnParry;
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        gameInputs.Player.Movement.performed -= SetDirection;
        gameInputs.Player.Movement.canceled -= SetDirection;

        gameInputs.Player.Jump.started -= playerJump.OnJump;
        gameInputs.Player.Dash.started -= playerDash.OnDash;
        
        gameInputs.Skill.Spell.started -= OnAiming;
        gameInputs.Skill.Spell.canceled -= OnStopAiming;

        gameInputs.Skill.Parry.started -= playerParry.OnParry;

        gameInputs.Player.Disable(); // Disable inputs
        gameInputs.Skill.Disable();
    }
    #endregion

    #region Player Controller Methods
    void SetDirection(InputAction.CallbackContext context)
    {
        playerDirection = context.ReadValue<Vector2>();
        if (playerMovement != null) { playerMovement.Direction = playerDirection.x; }
        if (playerJump != null) { playerJump.Direction = Mathf.Min(0, playerDirection.y); }
        if (playerDash != null) { playerDash.Direction = playerDirection.normalized; }  

    }

    void OnAiming(InputAction.CallbackContext context)
    {
        gameInputs.Player.Jump.started -= playerJump.OnJump;
        gameInputs.Player.Dash.started -= playerDash.OnDash;
        gameInputs.Skill.Parry.started -= playerParry.OnParry;
        playerMovement.CanMove = false;
    }

    void OnStopAiming(InputAction.CallbackContext context)
    {
        gameInputs.Player.Jump.started += playerJump.OnJump;
        gameInputs.Player.Dash.started += playerDash.OnDash;
        gameInputs.Skill.Parry.started += playerParry.OnParry;
        playerMovement.CanMove = true;
    }

    //Considerar Apagar se n�o for usar
    public Vector2 GetDirection()
    {
        return playerDirection;
    }
    #endregion
}
