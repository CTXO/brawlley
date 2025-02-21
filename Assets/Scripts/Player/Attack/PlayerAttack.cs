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
    public enum PlayerViewDirection
    {
        Right,
        Left
    }
    public abstract class PlayerAttack : MonoBehaviour
    {
        #region Player Attack Resources
        [Header("Player Attack Resources")]
        public Player player;
        #endregion

        #region Player Attack Data
        [Header("Player Attack Data")]
        public float damage = 10f;
        public float knockbackForce = 5f;

        [Tooltip("The time in seconds between each attack.")]
        public float cooldown = 1f;


        [Header("Player Attack Status")]
        public AttackStatus status = AttackStatus.Ready;
        public PlayerViewDirection viewDirection = PlayerViewDirection.Right;
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

        public void SetPlayerViewDirection(Vector2 direction)
        {
            if (direction.x > 0)
                viewDirection = PlayerViewDirection.Right;
            if (direction.x < 0)
                viewDirection = PlayerViewDirection.Left;
        }
        #endregion
    }
}