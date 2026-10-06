using UnityEngine;

public class Caja : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D choque)
    {
        Vector2 normal = choque.GetContact(0).normal;
        if (choque.gameObject.CompareTag("Player") && normal.y > 0.5f)
        {
            Destroy(gameObject);
        }
    }
}
