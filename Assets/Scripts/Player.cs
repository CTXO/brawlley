using UnityEngine;

namespace Brawlley
{
    public class Player : MonoBehaviour
    {
        [SerializeField] string team;
        public string Team { get => team; set => team = value; }
    }
}
