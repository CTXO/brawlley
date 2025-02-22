using Brawlley.Attacks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Brawlley.Player
{
    public class PlayerSpell : PlayerAttack
    {
        #region Player Spell Resources
        [Header("Player Spell Resources")]
        public GameObject spellPrefab;
        public Transform spellSpawnPoint;
        #endregion

        #region Player Spell Data
        [Header("Player Spell Data")]
        [SerializeField] float castTime = 0.5f;
        public float CastTime { get => castTime; set => castTime = value; }
        [SerializeField] float travelSpeed = 1f;
        public float TravelSpeed { get => travelSpeed; set => travelSpeed = value; }
        [SerializeField] float duration = 2f;
        public float Duration { get => duration; set => duration = value; }
        #endregion

        #region MonoBehaviour Lifecycle Methods
        protected override void Awake()
        {
            base.Awake();
            GameInputs.Attack.Spell.performed += OnAttack;
        }
        #endregion

        #region Player Spell Methods
        public override void OnAttack(InputAction.CallbackContext context)
        {
            if (status == AttackStatus.Ready)
            {
                Vector2 attackDirection = GameInputs.Player.Movement.ReadValue<Vector2>();
                Debug.Log($"Attack Direction: {attackDirection}");
                GameObject spellObject = Instantiate(spellPrefab, spellSpawnPoint.position, Quaternion.identity);

                Spell spell = spellObject.GetComponent<Spell>();
                spell.Damage = Damage;
                spell.KnockbackForce = KnockbackForce;
                spell.IgnoreTeam = player.Team;
                spell.Speed = travelSpeed;
                spell.ApplyForce(attackDirection);
                spell.StartDestroyAfterDuration();

                StartCooldown();
            }
        }
        #endregion
    }
}
