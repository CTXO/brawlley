using UnityEngine;
using UnityEngine.InputSystem;

namespace Brawlley
{
    public class Player : MonoBehaviour
    {
        #region Player Resources
        [Header("Player Resources")]
        [SerializeField] PlayerController playerController;
        #endregion

        #region Player Data
        [Header("Player Data")]
        [SerializeField] string team;
        public string Team { get => team; set => team = value; }

        public GameInputs GameInputs => playerController.GameInputs;
        #endregion
    }
}
