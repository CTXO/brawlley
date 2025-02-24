using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 15f;  // Speed of the dash
    public float dashDuration = 0.2f;  // How long the dash lasts
    public float dashCooldown = 0.5f;  // Time before another dash is allowed

    private Rigidbody2D playerRb;
    private bool isDashing;
    private bool canDash = true;

    private Vector2 dashDirection;
    private Vector2 inputDirection;
    private float dashTime;
    private PlayerSurfaceDetection surfaceDetector;

    public void OnDash(InputAction.CallbackContext context)
    {
        if (isDashing || !canDash) return;

        if (context.started)
        {
            StartDash(inputDirection);
        }
        
    }
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        surfaceDetector = GetComponent<PlayerSurfaceDetection>();
        
    }


    void FixedUpdate()
    {
        if (isDashing)
        {
            playerRb.linearVelocity = dashDirection * dashSpeed;
        }
    }

    private void StartDash(Vector2 direction)
    {
        isDashing = true;
        canDash = false;
        dashDirection = direction;
        dashTime = dashDuration;

        // Disable gravity during dash for a smooth effect
        playerRb.gravityScale = 0;

        Invoke(nameof(EndDash), dashDuration);
        Invoke(nameof(ResetDashCooldown), dashCooldown);
    }

    private void EndDash()
    {
        isDashing = false;
        playerRb.gravityScale = 1;  // Restore gravity
    }

    private void ResetDashCooldown()
    {
        canDash = true;
    }

    public void UpdateDirection(Vector2 direction)
    {
        inputDirection = direction;
    }

    public bool GetGravityCancel() { return isDashing && dashDirection == Vector2.zero; }
    public bool GetIsDashing() { return isDashing; }
}