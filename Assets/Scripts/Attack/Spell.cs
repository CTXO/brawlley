using System.Collections;
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

        [SerializeField] float duration = 2f;
        public float Duration { get => duration; set => duration = value; }
        #endregion

        #region Spell Methods
        public void ApplyForce(Vector2 direction)
        {
            if (spellRigidbody == null)
                spellRigidbody.AddForce(new Vector2(direction.x, direction.y) * speed, ForceMode2D.Force);
        }

        public void OnTagCollisionDestroy(string tag)
        {
            if (currentCollision != null && currentCollision.CompareTag(tag))
                Destroy(gameObject);
        }

        IEnumerator DestroyAfterDuration()
        {
            yield return new WaitForSeconds(duration);
            Destroy(gameObject);
        }

        public void StartDestroyAfterDuration() => StartCoroutine(DestroyAfterDuration());

        public void ResetDestroyAfterDuration()
        {
            StopCoroutine(DestroyAfterDuration());
            StartCoroutine(DestroyAfterDuration());
        }
        #endregion
    }
}
