using UnityEngine;
using UnityEngine.InputSystem;

namespace Brawlley
{
    public class Player : MonoBehaviour
    {
        #region Player Resources
        [Header("Player Resources")]
        [SerializeField] PlayerController playerController;
        [SerializeField] GameObject playerAxis;
        #endregion

        #region Player Data
        [Header("Player Data")]
        [SerializeField] string team;
        public string Team { get => team; set => team = value; }

        public GameInputs GameInputs => playerController.GameInputs;
        #endregion

        #region MonoBehaviour Lifecycle Methods
        void Awake()
        {
            if (playerAxis != null)
                GameInputs.Player.Movement.performed += UpdateAxisDirection;
        }
        #endregion

        #region Player Methods
        public void UpdateAxisDirection(InputAction.CallbackContext context)
        {
            Vector2 movement = context.ReadValue<Vector2>();
            if (movement.x != 0)
                playerAxis.transform.rotation = Quaternion.Euler(0, movement.x > 0 ? 0 : 180, 0);
        }
        #endregion
    }
}
