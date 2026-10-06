using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    [SerializeField] Transform[] paradas;
    [SerializeField] float velocidad = 2f;

    int siguiente;
    Rigidbody2D pasajero;

    void FixedUpdate()
    {
        Vector2 antes = transform.position;
        Vector3 destino = paradas[siguiente].position;
        transform.position = Vector2.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);
        if (pasajero != null)
        {
            pasajero.position += (Vector2)transform.position - antes;
        }
        if (transform.position == destino)
        {
            siguiente++;
            if (siguiente == paradas.Length)
            {
                siguiente = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D choque)
    {
        if (choque.gameObject.CompareTag("Player"))
        {
            pasajero = choque.rigidbody;
        }
    }

    void OnCollisionExit2D(Collision2D choque)
    {
        if (choque.gameObject.CompareTag("Player"))
        {
            pasajero = null;
        }
    }
}
