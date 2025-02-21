using UnityEngine;

public class PlayerSurfaceDetection : MonoBehaviour
{
    
    private bool onGround;
    private bool onWall;
    private Vector2 facingDirection;
    private Vector3 groundOffset;
    private Vector3 wallOffset;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private Vector2 colliderSizeGround;
    [SerializeField] private Vector2 colliderSizeAir;
    [SerializeField] private float colliderRadiusGround;
    [SerializeField] private float colliderRadiusAir;


    [Header("Collider Settings")]
    [SerializeField][Tooltip("Length of the ground-checking collider")] private float groundLength = 0.95f;
    [SerializeField][Tooltip("Length of the ground-checking collider")] private float wallLength = 0.65f;
    [SerializeField][Tooltip("Distance between the ground-checking colliders")] private Vector3 colliderOffset;

    [Header("Layer Masks")]
    [SerializeField][Tooltip("Which layers are read as the ground")] private LayerMask groundLayer;

    private void Awake()
    {
        groundOffset = new Vector3(colliderOffset.x, 0, 0);
        wallOffset = new Vector3(0, colliderOffset.y, 0);
    }

    private void Update()
    {
        facingDirection = new Vector2(transform.localScale.x, 0);
        //Determine if the player is stood on objects on the ground layer, using a pair of raycasts
        onGround = Physics2D.Raycast(transform.position + groundOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - groundOffset, Vector2.down, groundLength, groundLayer);
        onWall = Physics2D.Raycast(transform.position + wallOffset, facingDirection, wallLength, groundLayer) || Physics2D.Raycast(transform.position - wallOffset, facingDirection, wallLength, groundLayer);
    }

    private void FixedUpdate()
    {
        if(onGround)
        {
            playerCollider.size = colliderSizeGround;
            playerCollider.edgeRadius = colliderRadiusGround;
        }
        else
        {
            playerCollider.size = colliderSizeAir;
            playerCollider.edgeRadius = colliderRadiusAir;
        }
    }

    private void OnDrawGizmos()
    {
        // Draw the ground detection rays
        Gizmos.color = onGround ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position + groundOffset, transform.position + groundOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - groundOffset, transform.position - groundOffset + Vector3.down * groundLength);

        // Draw the wall detection rays
        Gizmos.color = onWall ? Color.blue : Color.red;
        Gizmos.DrawLine(transform.position + wallOffset, transform.position + wallOffset + facingDirection.x * wallLength * Vector3.right);
        Gizmos.DrawLine(transform.position - wallOffset , transform.position - wallOffset + facingDirection.x * wallLength * Vector3.right);


    }

    //Send ground detection to other scripts
    public bool GetOnGround() { return onGround; }
    public bool GetOnWall() { return onWall; }
    public float GetFacingDirection() { return facingDirection.x; }
}
