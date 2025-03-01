using Brawlley.Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMelee : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector2 attackDirection;
    private Animator playerAnim; 
    [SerializeField] bool canAttack = true;
    [SerializeField] float meleeModifier = 1f;
    [SerializeField] CapsuleCollider2D meleeProjectileCollider;

    public Vector2 AttackDirection { get => attackDirection; set => attackDirection = value; }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && canAttack)
        {
            canAttack = false;
            playerAnim.SetTrigger("MeleeTrigger");
        }
        
    }

    void Start()
    {
        playerAnim = GetComponent<Animator>();
        canAttack = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Spell"))
        {
            if (meleeProjectileCollider.IsTouching(collision))
            {
                Debug.Log("Rebateu");   
                Spell spell = collision.GetComponent<Spell>();
                if (attackDirection == Vector2.zero)
                {
                    Debug.Log("Rebateu paradodo");  
                    spell.Attack(new Vector2(transform.localScale.x, -1) * meleeModifier);
                }
                else
                {
                    spell.Attack(attackDirection * meleeModifier);
                }
                return;
            }
        }
    }
}
