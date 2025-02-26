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
        #endregion

        #region MonoBehaviour Lifecycle Methods
        protected override void Start()
        {
            base.Start();
            GameInputs.Skill.Spell.canceled += OnAttack;
        }
        #endregion

        #region Player Spell Methods
        public override void OnAttack(InputAction.CallbackContext context)
        {
            if (status == AttackStatus.Ready)
            {
                Vector2 attackDirection = GameInputs.Player.Movement.ReadValue<Vector2>();

                if (attackDirection == Vector2.zero)
                    attackDirection.x = direction == AttackDirection.Right ? 1f : -1f;

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
