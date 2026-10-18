using UnityEngine;

public class Arrastrable : MonoBehaviour
{
    [SerializeField] int ordenAgarrado = 10;

    SpriteRenderer dibujo;
    Rigidbody2D cuerpo;
    int ordenNormal;
    Vector3 inicio;
    Vector3 destino;
    bool agarrado;

    void Start()
    {
        dibujo = GetComponent<SpriteRenderer>();
        cuerpo = GetComponent<Rigidbody2D>();
        ordenNormal = dibujo.sortingOrder;
    }

    void FixedUpdate()
    {
        if (agarrado)
        {
            cuerpo.MovePosition(destino);
        }
    }

    public void Agarrar()
    {
        inicio = transform.position;
        agarrado = true;
        dibujo.sortingOrder = ordenAgarrado;
    }

    public void Mover(Vector3 punto)
    {
        destino = punto;
    }

    public void Soltar(Vector2 velocidad)
    {
        agarrado = false;
        dibujo.sortingOrder = ordenNormal;
        if (cuerpo.bodyType == RigidbodyType2D.Dynamic)
        {
            cuerpo.linearVelocity = velocidad;
        }
    }

    public void Volver()
    {
        transform.position = inicio;
        cuerpo.linearVelocity = Vector2.zero;
    }

    public bool EstaAgarrado()
    {
        return agarrado;
    }
}
