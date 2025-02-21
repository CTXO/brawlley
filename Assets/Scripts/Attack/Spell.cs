using UnityEngine;
using UnityEngine.Events;

namespace Brawlley.Attacks
{
    public class Spell : Attack
    {
        #region Spell Data
        [Header("Spell Data")]
        [SerializeField] float manaCost = 1f;
        public float ManaCost => manaCost;

        [Header("Events")]
        [SerializeField] UnityEvent onTriggerEnterEvent;
        Collider2D currentCollision;
        #endregion

        public void OnTriggerEnter2D(Collider2D collision)
        {
            currentCollision = collision;
            onTriggerEnterEvent.Invoke();
            currentCollision = null;
        }

        public void OnPlayerCollisionDealDamage()
        {
            if (currentCollision != null && currentCollision.CompareTag("Player"))
            {
                if (currentCollision.TryGetComponent<PlayerHealth>(out var playerHealth))
                    playerHealth.Health -= damage;
            }
        }
    }
}
