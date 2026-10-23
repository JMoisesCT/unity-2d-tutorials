using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 10f;

    int daño;

    public void Lanzar(Vector2 direccion, int dañoDelHeroe)
    {
        daño = dañoDelHeroe;
        transform.up = direccion;
        GetComponent<Rigidbody2D>().linearVelocity = direccion * velocidad;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.isTrigger)
        {
            return;
        }

        if (otro.TryGetComponent(out Heroe heroe))
        {
            return;
        }

        if (otro.TryGetComponent(out Vida vida))
        {
            vida.RecibirDaño(daño);
        }
        Destroy(gameObject);
    }
}
