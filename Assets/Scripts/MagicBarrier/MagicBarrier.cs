using UnityEngine;

public class MagicBarrier : MonoBehaviour
{
    public float lifetime = 10f;

    void Start()
    {
        // Destroy this game object after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

}
