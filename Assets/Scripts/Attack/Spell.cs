using UnityEngine;

namespace Brawlley.Attacks
{
    public class Spell : Attack
    {
        #region Spell Resources
        [Header("Spell Resources")]
        [SerializeField] Rigidbody2D spellRigidbody;
        #endregion

        #region Spell Data
        [Header("Spell Data")]
        [SerializeField] float speed = 1f;
        public float Speed { get => speed; set => speed = value; }
        #endregion

        #region Spell Methods
        public void ApplyForce(Vector2 direction)
        {
            if (spellRigidbody == null)
                spellRigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
        }
        #endregion
    }
}
