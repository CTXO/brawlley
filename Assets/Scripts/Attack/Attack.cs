using UnityEngine;

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
        #endregion
    }
}
