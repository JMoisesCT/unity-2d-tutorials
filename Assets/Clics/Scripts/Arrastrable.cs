using UnityEngine;
using System.Collections;

public class Arrastrable : MonoBehaviour
{
    [SerializeField] int ordenAgarrado = 10;

    [Header("Crecer")]
    [Tooltip("Tamaño con el mouse encima (1 = normal)")]
    [SerializeField] float escalaEncima = 1.15f;
    [Tooltip("Tamaño mientras la arrastras")]
    [SerializeField] float escalaAgarrado = 1.3f;
    [Tooltip("Qué tan rápido cambia de tamaño")]
    [SerializeField] float rapidez = 12f;

    [Header("Volver")]
    [Tooltip("Segundos que tarda en volver a su lugar")]
    [SerializeField] float duracion = 0.4f;
    [Tooltip("Grados que gira al sacudirse")]
    [SerializeField] float giro = 15f;

    SpriteRenderer dibujo;
    Rigidbody2D cuerpo;
    int ordenNormal;
    Vector3 escalaNormal;
    Vector3 inicio;
    Vector3 destino;
    bool agarrado;
    bool resaltado;
    bool volviendo;

    void Start()
    {
        dibujo = GetComponent<SpriteRenderer>();
        cuerpo = GetComponent<Rigidbody2D>();
        ordenNormal = dibujo.sortingOrder;
        escalaNormal = transform.localScale;
    }

    void Update()
    {
        float escala = 1;
        if (agarrado)
        {
            escala = escalaAgarrado;
        }
        else if (resaltado)
        {
            escala = escalaEncima;
        }
        Vector3 objetivo = escalaNormal * escala;
        transform.localScale = Vector3.Lerp(transform.localScale, objetivo, rapidez * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (agarrado)
        {
            cuerpo.MovePosition(destino);
        }
    }

    public void Resaltar(bool encima)
    {
        resaltado = encima;
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
        StartCoroutine(VolverSuave());
    }

    IEnumerator VolverSuave()
    {
        volviendo = true;
        cuerpo.simulated = false;

        for (int i = 0; i < 3; i++)
        {
            transform.rotation = Quaternion.Euler(0, 0, giro);
            yield return new WaitForSeconds(0.05f);
            transform.rotation = Quaternion.Euler(0, 0, -giro);
            yield return new WaitForSeconds(0.05f);
        }
        transform.rotation = Quaternion.identity;

        Vector3 desde = transform.position;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / duracion;
            transform.position = Vector3.Lerp(desde, inicio, t);
            yield return null;
        }

        cuerpo.simulated = true;
        cuerpo.linearVelocity = Vector2.zero;
        volviendo = false;
    }

    public bool EstaAgarrado()
    {
        return agarrado;
    }

    public bool EstaVolviendo()
    {
        return volviendo;
    }
}
