using UnityEngine;
using UnityEngine.Events;

namespace Brawlley.Attacks
{
    public abstract class Attack : MonoBehaviour
    {
        #region Attack Data
        [Header("Attack Data")]
        [SerializeField] protected float damage = 1f;
        public float Damage => damage;

        [SerializeField] protected float knockbackForce = 1f;
        public float KnockbackForce => knockbackForce;

        [SerializeField] protected string ignoreTeam;
        public string IgnoreTeam { get => ignoreTeam; set => ignoreTeam = value; }

        [Header("Events")]
        [SerializeField] UnityEvent onTriggerEnterEvent;
        protected Collider2D currentCollision;
        #endregion

        #region Collision Methods
        public void OnTriggerEnter2D(Collider2D collision)
        {
            currentCollision = collision;
            onTriggerEnterEvent.Invoke();
            currentCollision = null;
        }
        #endregion

        #region Attack Methods
        public virtual void OnPlayerCollisionDealDamage()
        {
            if (currentCollision != null && currentCollision.CompareTag("Player"))
            {
                if (currentCollision.TryGetComponent<Player>(out var collidedPlayer))
                {
                    if (collidedPlayer.Team == IgnoreTeam)
                        return;

                    if (currentCollision.TryGetComponent<PlayerHealth>(out var playerHealth))
                        playerHealth.Health -= damage;
                }

            }
        }
        #endregion
    }
}
