using UnityEngine;

public enum Mejora { Disparo, Escudo }

public class PowerUp : MonoBehaviour
{
    [Tooltip("Qué le da a la nave")]
    [SerializeField] Mejora mejora;
    [Tooltip("Unidades por segundo, hacia la izquierda")]
    [SerializeField] float velocidad = 2f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * velocidad;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.TryGetComponent(out Nave nave))
        {
            if (mejora == Mejora.Disparo)
            {
                nave.MejorarDisparo();
            }
            else
            {
                nave.PonerEscudo();
            }
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
