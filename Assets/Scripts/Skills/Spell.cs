using System.Collections;
using UnityEngine;

namespace Brawlley.Attacks
{
    public class Spell : Attack
    {
        #region Resources
        [Header("Spell Resources")]
        [SerializeField] Rigidbody2D spellRigidbody;
        #endregion

        #region Data
        [Header("Spell Data")]
        public float speed = 1f;
        public float timeToActivate = 0.1f;
        public Vector2 direction;
        [SerializeField] readonly float parriedProjectileVerticalSpeed = 0.6f;
        [SerializeField] readonly float parriedProjectileHeight = 10f;
        [SerializeField] readonly float parriedProjectileHorizontalSpeed = 2f;

        private float parriedProjectileVelocity;
        private float gravityScale;
        private CircleCollider2D spellCollider;
        #endregion

        #region Spell Methods
        private void Start()
        {
            spellCollider = GetComponent<CircleCollider2D>();
            Invoke(nameof(ActivateCollision), timeToActivate);
        }

        private void ActivateCollision()
        {
            spellCollider.enabled = true;
        }

        private void ApplyGravity()
        {
            float gravity = -(2 * parriedProjectileHeight) / (parriedProjectileVerticalSpeed * parriedProjectileVerticalSpeed);
            gravityScale = gravity / Physics2D.gravity.y;
            spellRigidbody.gravityScale = gravityScale;

            parriedProjectileVelocity = Mathf.Abs(gravity) * parriedProjectileVerticalSpeed;
        }

        public void ApplyForce()
        {
            if (spellRigidbody != null)
                spellRigidbody.AddForce(direction * speed, ForceMode2D.Impulse);
        }

        public void Parry()
        {
            ApplyGravity();
            spellRigidbody.linearVelocity = new Vector2(parriedProjectileHorizontalSpeed * direction.x, parriedProjectileVelocity);
        }
        
        public virtual void OnPlayerCollisionDealKnockback()
        {
            if (currentCollision != null && currentCollision.CompareTag("Player"))
            {
                if (currentCollision.TryGetComponent<Player>(out var collidedPlayer))
                {
                    if (collidedPlayer.Team == ignoreTeam)
                        return;

                    if (currentCollision.TryGetComponent<Rigidbody2D>(out var playerRigidbody))
                        playerRigidbody.AddForce(direction * knockbackForce, ForceMode2D.Impulse);

                    Destroy(gameObject);
                }

            }
        }

        //public void OnTagCollisionDestroy(string tag)
        //{
        //    if (currentCollision != null && currentCollision.CompareTag(tag))
        //        Destroy(gameObject);
        //}

        //IEnumerator DestroyAfterDuration()
        //{
        //    yield return new WaitForSeconds(Duration);
        //    Destroy(gameObject);
        //}

        //public void StartDestroyAfterDuration() => StartCoroutine(DestroyAfterDuration());

        //public void ResetDestroyAfterDuration()
        //{
        //    StopCoroutine(DestroyAfterDuration());
        //    StartCoroutine(DestroyAfterDuration());
        //}
        #endregion
    }
}
