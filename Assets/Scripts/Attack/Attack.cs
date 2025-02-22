using UnityEngine;
using UnityEngine.Events;

namespace Brawlley.Attacks
{
    public abstract class Attack : MonoBehaviour
    {
        #region Data
        [Header("Attack Data")]
        [SerializeField] float damage = 1f;
        [SerializeField] float knockbackForce = 1f;
        [SerializeField] string ignoreTeam;

        [Header("Events")]
        [SerializeField] UnityEvent onTriggerEnterEvent;
        protected Collider2D currentCollision;
        #endregion

        #region Properties
        public float Damage { get => damage; set => damage = value; }
        public float KnockbackForce { get => knockbackForce; set => knockbackForce = value; }
        public string IgnoreTeam { get => ignoreTeam; set => ignoreTeam = value; }
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
                        playerHealth.Health += damage;

                    Destroy(gameObject);
                }

            }
        }
        #endregion
    }
}
