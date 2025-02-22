using Brawlley.Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Brawlley
{
    public class PlayerSpell : PlayerAttack
    {
        #region Resources
        [Header("Player Spell Resources")]
        public GameObject spellPrefab;
        public Transform spellSpawnPoint;
        #endregion

        #region Data
        [Header("Player Spell Data")]
        [SerializeField] float castTime = 0.5f;
        [SerializeField] float travelSpeed = 1f;
        [SerializeField] float duration = 2f;
        #endregion

        #region Properties
        public float CastTime { get => castTime; set => castTime = value; }
        public float TravelSpeed { get => travelSpeed; set => travelSpeed = value; }
        public float Duration { get => duration; set => duration = value; }
        #endregion

        #region MonoBehaviour Lifecycle Methods
        protected override void Start()
        {
            base.Start();
            GameInputs.Attack.Spell.performed += OnAttack;
        }
        #endregion

        #region Player Spell Methods
        public override void OnAttack(InputAction.CallbackContext context)
        {
            if (Status == AttackStatus.Ready)
            {
                Vector2 attackDirection = GameInputs.Player.Movement.ReadValue<Vector2>();
                if (attackDirection == Vector2.zero)
                    attackDirection.x = Direction == AttackDirection.Right ? 1f : -1f;
                Debug.Log($"Attack Direction: {attackDirection}");
                GameObject spellObject = Instantiate(spellPrefab, spellSpawnPoint.position, Quaternion.identity);

                Spell spell = spellObject.GetComponent<Spell>();
                spell.Damage = Damage;
                spell.KnockbackForce = KnockbackForce;
                spell.IgnoreTeam = player.Team;
                spell.Speed = travelSpeed;
                spell.Duration = duration;
                spell.Direction = attackDirection;
                spell.ApplyForce();
                spell.StartDestroyAfterDuration();

                StartCooldown();
            }
        }
        #endregion
    }
}
