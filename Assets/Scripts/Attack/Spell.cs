using UnityEngine;

namespace Brawlley.Attacks
{
    public class Spell : Attack
    {
        #region Spell Data
        [Header("Spell Data")]
        [SerializeField] float manaCost = 1f;
        public float ManaCost => manaCost;
        #endregion

        
    }
}
