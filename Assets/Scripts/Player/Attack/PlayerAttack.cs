using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Brawlley
{
    public enum AttackStatus { Ready, Cooldown }
    public enum AttackDirection { Left, Right }

    public abstract class PlayerAttack : MonoBehaviour
    {
        #region Resources
        [Header("Player Attack Resources")]
        public Player player;
        protected GameInputs GameInputs => player.GameInputs;
        #endregion

        #region Data
        [Header("Player Attack Data")]
        [SerializeField] float damage = 10f;
        [SerializeField] float knockbackForce = 5f;

        [Tooltip("The time in seconds between each attack.")]
        [SerializeField] float cooldown = 1f;
        [SerializeField] AttackStatus status = AttackStatus.Ready;
        [SerializeField] AttackDirection direction = AttackDirection.Right;
        #endregion

        #region Properties
        protected float Damage { get => damage; set => damage = value; }
        protected float KnockbackForce { get => knockbackForce; set => knockbackForce = value; }
        protected float Cooldown { get => cooldown; set => cooldown = value; }
        protected AttackStatus Status { get => status; set => status = value; }
        protected AttackDirection Direction { get => direction; set => direction = value; }
        #endregion

        #region MonoBehaviour Lifecycle Methods
        protected virtual void Start()
        {
            if (player == null)
                player = GetComponent<Player>();
        }
        #endregion

        #region Player Attack Methods
        public virtual void OnAttack(InputAction.CallbackContext context) {}

        IEnumerator CooldownCoroutine()
        {
            yield return new WaitForSeconds(Cooldown);
            Status = AttackStatus.Ready;
        }

        protected void StartCooldown()
        {
            Status = AttackStatus.Cooldown;
            StartCoroutine(CooldownCoroutine());
        }

        protected void UpdateDirection(float x)
        {
            if (x < 0)
                Direction = AttackDirection.Left;
            if (x > 0)
                Direction = AttackDirection.Right;
        }
        #endregion
    }
}