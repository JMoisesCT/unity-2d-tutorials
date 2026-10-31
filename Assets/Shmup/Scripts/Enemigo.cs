using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Tooltip("Unidades por segundo, hacia la izquierda")]
    [SerializeField] float velocidad = 4f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * velocidad;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
