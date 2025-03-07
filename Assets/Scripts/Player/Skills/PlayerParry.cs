using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerParry : MonoBehaviour
{
    
    [SerializeField] GameObject barrierPrefab;
    [SerializeField] float barrierOffset = 1f;
    [SerializeField] float parryCooldown = 1f;
    private PlayerSurfaceDetection surfaceDetector;
    private PlayerDash dash;
    private bool canParry;

    private void Start()
    {
        surfaceDetector = GetComponent<PlayerSurfaceDetection>();
        dash = GetComponent<PlayerDash>();
        canParry = true;
    }

    public void OnParry(InputAction.CallbackContext context)
    {
        // O player só pode subir uma barreira no chão ou se der um Dash neutro (Gravity Cancel)
        if (context.started && canParry && (surfaceDetector.GetOnGround() || dash.GravityCancel))
        {
            SummonBarrier();
            StartCoroutine(Cooldown());
        }
    }
    public void OnRemoteParry()
    {
        // O player só pode subir uma barreira no chão ou se der um Dash neutro (Gravity Cancel)
        if (canParry && (surfaceDetector.GetOnGround() || dash.GravityCancel))
        {
            SummonBarrier();
            StartCoroutine(Cooldown());
        }
    }

    // Gerar uma barreira na frente do player numa margem controlada pelo barrier Offset
    private Vector3 GetBarrierPosition()
    {
        return new Vector3(transform.position.x + barrierOffset * surfaceDetector.GetFacingDirection(), transform.position.y, transform.position.z);
    }

    private void SummonBarrier()
    {
        Instantiate(barrierPrefab, GetBarrierPosition(), Quaternion.identity);
    }
    private IEnumerator Cooldown()
    {
        canParry = false;
        yield return new WaitForSeconds(parryCooldown);
        canParry = true;
    }
}
