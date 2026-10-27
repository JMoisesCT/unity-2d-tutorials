using System.Collections;
using UnityEngine;

public class Punk : MonoBehaviour, IGolpeable
{
    enum Estado { Acercarse, Atacar, Herido, Caido }

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

    [Header("Ataque")]
    [Tooltip("Segundos entre un puñetazo y el siguiente")]
    [SerializeField] float espera = 1.5f;
    [Tooltip("Segundos que dura el puñetazo; mientras tanto no se mueve")]
    [SerializeField] float duracionGolpe = 0.6f;
    [Tooltip("Distancia máxima hacia los lados para que el puñetazo llegue")]
    [SerializeField] float alcance = 2f;
    [Tooltip("Diferencia de profundidad máxima para que el puñetazo llegue")]
    [SerializeField] float carril = 0.4f;
    [Tooltip("Vida que quita cada puñetazo")]
    [SerializeField] int daño = 1;

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
    Estado estado;
    float ultimoGolpe;

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

    public void Perseguir(Transform peleadora)
    {
        objetivo = peleadora;
    }

    void FixedUpdate()
    {
        switch (estado)
        {
            case Estado.Acercarse:
                Acercarse();
                break;
            case Estado.Atacar:
                if (Time.time - ultimoGolpe > duracionGolpe)
                {
                    estado = Estado.Acercarse;
                }
                break;
            case Estado.Herido:
                Retroceder();
                if (empuje == 0)
                {
                    estado = Estado.Acercarse;
                }
                break;
            case Estado.Caido:
                Retroceder();
                break;
        }
    }

    void Acercarse()
    {
        Vector2 destino = new Vector2(objetivo.position.x + lado * distancia, objetivo.position.y);

        Vector2 siguiente = cuerpo.position;
        siguiente.x = Mathf.MoveTowards(siguiente.x, destino.x, velocidad * Time.deltaTime);
        siguiente.y = Mathf.MoveTowards(siguiente.y, destino.y, velocidadProfundidad * Time.deltaTime);

        bool camina = siguiente != cuerpo.position;
        animador.SetBool("Caminando", camina);
        dibujo.flipX = objetivo.position.x > cuerpo.position.x;

        cuerpo.MovePosition(siguiente);

        if (!camina && Time.time - ultimoGolpe > espera)
        {
            ultimoGolpe = Time.time;
            animador.SetTrigger("Golpear");
            estado = Estado.Atacar;
        }
    }

    void Retroceder()
    {
        Vector2 atras = cuerpo.position + new Vector2(empuje * Time.deltaTime, 0);
        empuje = Mathf.MoveTowards(empuje, 0, freno * Time.deltaTime);
        cuerpo.MovePosition(atras);
    }

    public void Recibir(float fuerza, float hacia, int daño)
    {
        vida.RecibirDaño(daño);
        empuje = fuerza * hacia;
        if (vida.Vivo())
        {
            estado = Estado.Herido;
            animador.SetTrigger("Herido");
        }
        else
        {
            StartCoroutine(Caer());
        }
    }

    public void Golpear()
    {
        float lejos = Mathf.Abs(objetivo.position.x - cuerpo.position.x);
        float otroCarril = Mathf.Abs(objetivo.position.y - cuerpo.position.y);
        if (lejos < alcance && otroCarril < carril)
        {
            Debug.Log(name + " le pega a la peleadora");
            objetivo.GetComponent<Peleadora>().Recibir(daño);
        }
        else
        {
            Debug.Log(name + " falla");
        }
    }

    IEnumerator Caer()
    {
        Debug.Log(name + " cae");
        estado = Estado.Caido;
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
