using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    void OiggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // 待補上
        }
    }
}
