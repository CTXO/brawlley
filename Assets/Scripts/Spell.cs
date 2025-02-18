using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Brawlley
{
    public class Spell : PlayerAttack
    {
        #region Properties
        [SerializeField] PlayerMovement playerMovement;
        [Tooltip("The time in seconds the spell will last.")]
        public float duration = 1f;
        public float speed = 5f;

        public GameObject spellPrefab;
        public Transform spellSpawnTransform;

        Vector2 direction;

        GameInputs gameInputs;
        #endregion

        #region MonoBehaviour Lifecycle Methods
        void Awake()
        {
            gameInputs = new GameInputs();
            gameInputs.Attack.Enable();
            gameInputs.Attack.Spell.performed += OnAttack;
        }

        void Update()
        {
            direction.x = playerMovement.directionX;
            SetPlayerViewDirection(direction);
            direction.x = viewDirection == PlayerViewDirection.Right ? 1 : -1;
        }
        #endregion

        #region Spell Methods
        public override void OnAttack(InputAction.CallbackContext context)
        {
            if (status == AttackStatus.Ready)
            {
                GameObject spellInstance = Instantiate(spellPrefab, spellSpawnTransform.position, spellSpawnTransform.rotation);

                SpellTravel(spellInstance, direction);

                StartCoroutine(DestroyAfterDuration(spellInstance));
                StartCooldown();
            }
        }

        private IEnumerator DestroyAfterDuration(GameObject spellInstance)
        {
            yield return new WaitForSeconds(duration);
            Destroy(spellInstance);
        }

        void SpellTravel(GameObject spellInstance, Vector2 direction)
        {
            Rigidbody2D spellRigidbody = spellInstance.GetComponent<Rigidbody2D>();
            StartCoroutine(SpellTravelCoroutine(spellRigidbody, direction));
        }

        IEnumerator SpellTravelCoroutine(Rigidbody2D spellRigidbody, Vector2 direction)
        {
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                spellRigidbody.linearVelocity = direction * speed;
                elapsedTime += Time.fixedDeltaTime;
                yield return null;
            }
        }

        #endregion
    }
}
