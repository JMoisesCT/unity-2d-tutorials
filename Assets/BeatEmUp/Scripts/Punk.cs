using System.Collections;
using UnityEngine;

public class Punk : MonoBehaviour
{
    [Header("Persecución")]
    [Tooltip("La peleadora a la que persigue")]
    [SerializeField] Transform objetivo;
    [Tooltip("A cuántas unidades de la peleadora se detiene")]
    [SerializeField] float distancia = 1.5f;

    [Header("Movimiento")]
    [Tooltip("Unidades por segundo hacia la izquierda o la derecha")]
    [SerializeField] float velocidad = 2.5f;
    [Tooltip("Unidades por segundo hacia el fondo o hacia el frente")]
    [SerializeField] float velocidadProfundidad = 1.5f;

    [Header("Golpes")]
    [Tooltip("Cuánta velocidad de empujón pierde cada segundo")]
    [SerializeField] float freno = 15f;

    [Header("KO")]
    [Tooltip("Segundos que queda tendido antes de parpadear")]
    [SerializeField] float tendido = 1f;
    [Tooltip("Cuántas veces parpadea antes de desaparecer")]
    [SerializeField] int parpadeos = 5;

    Rigidbody2D cuerpo;
    float lado;
    SpriteRenderer dibujo;
    Animator animador;
    float empuje;
    Vida vida;
    bool caido;

    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        vida = GetComponent<Vida>();

        lado = 1;
        if (cuerpo.position.x < objetivo.position.x)
        {
            lado = -1;
        }
    }

    void FixedUpdate()
    {
        if (empuje != 0)
        {
            Vector2 atras = cuerpo.position + new Vector2(empuje * Time.deltaTime, 0);
            empuje = Mathf.MoveTowards(empuje, 0, freno * Time.deltaTime);
            cuerpo.MovePosition(atras);
            return;
        }

        if (caido)
        {
            return;
        }

        Vector2 destino = new Vector2(objetivo.position.x + lado * distancia, objetivo.position.y);

        Vector2 siguiente = cuerpo.position;
        siguiente.x = Mathf.MoveTowards(siguiente.x, destino.x, velocidad * Time.deltaTime);
        siguiente.y = Mathf.MoveTowards(siguiente.y, destino.y, velocidadProfundidad * Time.deltaTime);

        bool camina = siguiente != cuerpo.position;
        animador.SetBool("Caminando", camina);
        dibujo.flipX = objetivo.position.x > cuerpo.position.x;

        cuerpo.MovePosition(siguiente);
    }

    public void Recibir(float fuerza, float hacia, int daño)
    {
        vida.RecibirDaño(daño);
        empuje = fuerza * hacia;
        if (vida.Vivo())
        {
            animador.SetTrigger("Herido");
        }
        else
        {
            StartCoroutine(Caer());
        }
    }

    IEnumerator Caer()
    {
        Debug.Log(name + " cae");
        caido = true;
        GetComponent<Collider2D>().enabled = false;
        animador.SetTrigger("Caer");
        yield return new WaitForSeconds(tendido);

        for (int i = 0; i < parpadeos; i++)
        {
            dibujo.enabled = false;
            yield return new WaitForSeconds(0.1f);
            dibujo.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
