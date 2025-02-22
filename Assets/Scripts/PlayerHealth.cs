using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 0f;
    public float Health { get => health; set => health = value; }
}
