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
        [SerializeField] protected Player player;
        //protected GameInputs GameInputs => player.GameInputs;
        #endregion

        #region Data
        [Header("Player Attack Data")]
        [SerializeField] protected float damage = 10f;
        [SerializeField] protected float knockbackForce = 5f;

        [Tooltip("The time in seconds between each attack.")]
        [SerializeField] protected float cooldown = 1f;
        [SerializeField] protected AttackStatus status = AttackStatus.Ready;
        [SerializeField] protected AttackDirection direction = AttackDirection.Right;
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
            yield return new WaitForSeconds(cooldown);
            status = AttackStatus.Ready;
        }

        protected void StartCooldown()
        {
            status = AttackStatus.Cooldown;
            StartCoroutine(CooldownCoroutine());
        }

        protected void UpdateDirection(float x)
        {
            if (x < 0)
                direction = AttackDirection.Left;
            if (x > 0)
                direction = AttackDirection.Right;
        }
        #endregion
    }
}