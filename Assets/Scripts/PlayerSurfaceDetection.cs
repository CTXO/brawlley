using UnityEngine;

public class PlayerSurfaceDetection : MonoBehaviour
{
    public bool onGround;
    public bool onWall;

    [Header("Collider Settings")]
    [SerializeField][Tooltip("Length of the ground-checking collider")] private float groundLength = 0.95f;
    [SerializeField][Tooltip("Length of the ground-checking collider")] private float wallLength = 0.65f;
    [SerializeField][Tooltip("Distance between the ground-checking colliders")] private Vector3 colliderOffset;

    [Header("Layer Masks")]
    [SerializeField][Tooltip("Which layers are read as the ground")] private LayerMask groundLayer;


    private void Update()
    {
        //Determine if the player is stood on objects on the ground layer, using a pair of raycasts
        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
        onWall = Physics2D.Raycast(transform.position + colliderOffset, Vector2.right, wallLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.left, wallLength, groundLayer);
    }

    private void OnDrawGizmos()
    {
        // Draw the ground detection rays
        Gizmos.color = onGround ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);

        // Draw the wall detection rays
        Gizmos.color = onWall ? Color.blue : Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.right * wallLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.left * wallLength);

    }

    //Send ground detection to other scripts
    public bool GetOnGround() { return onGround; }
    public bool GetOnWall() { return !onGround && onWall; }
}
