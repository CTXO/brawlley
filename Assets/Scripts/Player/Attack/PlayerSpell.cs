using UnityEngine;

namespace Brawlley.Player
{
    public class PlayerSpell : PlayerAttack
    {
        #region Player Spell Resources
        [Header("Player Spell Resources")]
        public GameObject spellPrefab;
        public Transform spellSpawnPoint;
        #endregion

        #region Player Spell Data
        [Header("Player Spell Data")]
        [SerializeField] float castTime = 0.5f;
        public float CastTime { get => castTime; set => castTime = value; }
        [SerializeField] float travelSpeed = 1f;
        public float TravelSpeed { get => travelSpeed; set => travelSpeed = value; }
        [SerializeField] float duration = 2f;
        public float Duration { get => duration; set => duration = value; }
        #endregion
    }
}
