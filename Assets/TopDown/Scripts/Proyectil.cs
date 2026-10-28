using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 10f;

    int daño;
    Ataque arma;

    public void Lanzar(Vector2 direccion, int dañoDelHeroe, Ataque quien)
    {
        daño = dañoDelHeroe;
        arma = quien;
        transform.up = direccion;
        GetComponent<Rigidbody2D>().linearVelocity = direccion * velocidad;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.isTrigger)
        {
            return;
        }

        if (!arma.PuedeGolpear(otro))
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
