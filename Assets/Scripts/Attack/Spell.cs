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
        Collider2D curretCollision;
        #endregion

        public void OnTriggerEnter2D(Collider2D collision)
        {
            curretCollision = collision;
            onTriggerEnterEvent.Invoke();
            curretCollision = null;
        }
    }
}
