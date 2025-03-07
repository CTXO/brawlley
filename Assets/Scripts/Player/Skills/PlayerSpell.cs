using Brawlley.Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Brawlley
{
    public class PlayerSpell : PlayerAttack
    {
        #region Resources
        [Header("Player Spell Resources")]
        [SerializeField] GameObject spellPrefab;
        [SerializeField] Transform spellSpawnPoint;
        #endregion

        #region Data
        [Header("Player Spell Data")]
        [SerializeField] float travelSpeed = 1f;
        [SerializeField] float verticalCastGravity = 0.5f;
        private Vector2 attackDirection;
        #endregion

        #region MonoBehaviour Lifecycle Methods
        protected override void Start()
        {
            base.Start();
        }
        #endregion

        #region Player Spell Methods
        
        public Vector2 AttackDirection { get => attackDirection; set => attackDirection = value; }

        public override void OnAttack(InputAction.CallbackContext context)
        {
            if (status == AttackStatus.Ready)
            {
                

                if (attackDirection == Vector2.zero)
                    attackDirection.x = transform.localScale.x;

                GameObject spellObject = Instantiate(spellPrefab, spellSpawnPoint.position, Quaternion.identity);

                Spell spell = spellObject.GetComponent<Spell>();
                spell.damage = damage;
                spell.knockbackForce = knockbackForce;
                spell.ignoreTeam = player.Team;
                spell.speed = travelSpeed;
                spell.direction = attackDirection;
                spell.ApplyGravity(attackDirection.y > 0f ? verticalCastGravity : 0f);
                spell.ApplyForce();

                StartCooldown();
            }
        }
        public  void OnRemoteAttack()
        {
            if (status == AttackStatus.Ready)
            {


                if (attackDirection == Vector2.zero)
                    attackDirection.x = transform.localScale.x;

                GameObject spellObject = Instantiate(spellPrefab, spellSpawnPoint.position, Quaternion.identity);

                Spell spell = spellObject.GetComponent<Spell>();
                spell.damage = damage;
                spell.knockbackForce = knockbackForce;
                spell.ignoreTeam = player.Team;
                spell.speed = travelSpeed;
                spell.direction = attackDirection;
                spell.ApplyGravity(attackDirection.y > 0f ? verticalCastGravity : 0f);
                spell.ApplyForce();

                StartCooldown();
            }
        }
        #endregion
    }
}
