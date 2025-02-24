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
        [SerializeField] float speed = 1f;
        //[SerializeField] float duration = 2f;
        [SerializeField] float timeToActivate = 0.1f;
        [SerializeField] readonly float parriedProjectileVerticalSpeed = 0.6f;
        [SerializeField] readonly float parriedProjectileHeight = 10f;
        [SerializeField] readonly float parriedProjectileHorizontalSpeed = 2f;
        [SerializeField] Vector2 direction;

        private float parriedProjectileVelocity;
        private float gravityScale;
        private CircleCollider2D spellCollider;
        #endregion

        #region Properties
        public float Speed { get => speed; set => speed = value; }
        //public float Duration { get => duration; set => duration = value; }
        public Vector2 Direction { get => direction; set => direction = value; }
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
                spellRigidbody.AddForce(Direction * Speed, ForceMode2D.Impulse);
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
                    if (collidedPlayer.Team == IgnoreTeam)
                        return;

                    if (currentCollision.TryGetComponent<Rigidbody2D>(out var playerRigidbody))
                        playerRigidbody.AddForce(Direction * KnockbackForce, ForceMode2D.Impulse);

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
