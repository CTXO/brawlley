using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Brawlley
{
    public enum AttackStatus
    {
        Ready,
        Cooldown
    }

    public abstract class PlayerAttack : MonoBehaviour
    {
        #region Player Attack Resources
        [Header("Player Attack Resources")]
        public Player player;
        public GameInputs gameInputs;
        #endregion

        #region Player Attack Data
        [Header("Player Attack Data")]
        [SerializeField] float damage = 10f;
        public float Damage { get => damage; set => damage = value; }

        [SerializeField] float knockbackForce = 5f;
        public float KnockbackForce { get => knockbackForce; set => knockbackForce = value; }

        [Tooltip("The time in seconds between each attack.")]
        public float cooldown = 1f;
        public AttackStatus status = AttackStatus.Ready;
        #endregion

        #region MonoBehaviour Lifecycle Methods
        protected virtual void Awake()
        {
            gameInputs = new GameInputs();
            if (player == null)
                player = GetComponent<Player>();
        }
        #endregion

        #region Attack Methods
        public virtual void OnAttack(InputAction.CallbackContext context) {}

        IEnumerator CooldownCoroutine()
        {
            yield return new WaitForSeconds(cooldown);
            status = AttackStatus.Ready;
        }

        public void StartCooldown()
        {
            status = AttackStatus.Cooldown;
            StartCoroutine(CooldownCoroutine());
        }
        #endregion
    }
}