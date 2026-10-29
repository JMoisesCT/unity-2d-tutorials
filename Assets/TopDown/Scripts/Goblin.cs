using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Goblin : MonoBehaviour
{
    [Header("Movimiento")]
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 5f;

    [Header("Vida")]
    [Tooltip("En medios corazones: 6 = 3 corazones")]
    [SerializeField] int vidaMaxima = 6;
    [Tooltip("Segundos antes de volver a empezar")]
    [SerializeField] float espera = 1.5f;

    [Header("Esconderse")]
    [Tooltip("Hasta qué distancia encuentra una caja")]
    [SerializeField] float alcanceCaja = 1f;
    [Tooltip("Qué tanto se ve dentro de la caja (0 = nada, 1 = entero)")]
    [Range(0, 1)]
    [SerializeField] float opacidad = 0.4f;

    [Header("Monedas")]
    [Tooltip("El contador del HUD")]
    [SerializeField] ContadorMonedas contador;
    [Tooltip("El prefab de la moneda que suelta al recibir un golpe")]
    [SerializeField] Moneda moneda;
    [Tooltip("Cuántas monedas suelta con cada golpe")]
    [SerializeField] int perdidaPorGolpe = 2;
    [Tooltip("A qué distancia caen las monedas que suelta")]
    [SerializeField] float distancia = 1.5f;

    InputAction mover;
    InputAction esconder;
    Rigidbody2D cuerpo;
    SpriteRenderer dibujo;
    Animator animador;
    Vector2 direccion;
    int monedas;
    int juntadas;
    bool escondido;
    Vector2 afuera;

    void Start()
    {
        mover = InputSystem.actions.FindAction("Move");
        esconder = InputSystem.actions.FindAction("Interact");
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        GetComponent<Vida>().Empezar(vidaMaxima);
        Debug.Log("Récord: " + PlayerPrefs.GetInt("RecordMonedas", 0) + " monedas");
    }

    void Update()
    {
        if (esconder.WasPressedThisFrame())
        {
            if (escondido)
            {
                Salir();
            }
            else
            {
                BuscarCaja();
            }
        }

        if (escondido)
        {
            direccion = Vector2.zero;
            animador.SetBool("Corriendo", false);
            return;
        }

        direccion = mover.ReadValue<Vector2>();

        if (direccion.x != 0)
        {
            dibujo.flipX = direccion.x < 0;
        }

        animador.SetBool("Corriendo", direccion != Vector2.zero);
    }

    void FixedUpdate()
    {
        cuerpo.linearVelocity = direccion * velocidad;
    }

    void BuscarCaja()
    {
        Collider2D[] cerca = Physics2D.OverlapCircleAll(cuerpo.position, alcanceCaja);
        foreach (Collider2D tocado in cerca)
        {
            if (tocado.TryGetComponent(out Caja caja))
            {
                Esconderse(caja);
                return;
            }
        }
    }

    void Esconderse(Caja caja)
    {
        escondido = true;
        afuera = cuerpo.position;
        cuerpo.bodyType = RigidbodyType2D.Kinematic;
        cuerpo.position = caja.transform.position;
        Transparente(opacidad);
        Debug.Log("El goblin se mete en la caja");
    }

    void Salir()
    {
        escondido = false;
        cuerpo.bodyType = RigidbodyType2D.Dynamic;
        cuerpo.position = afuera;
        Transparente(1);
        Debug.Log("El goblin sale de la caja");
    }

    void Transparente(float alfa)
    {
        Color color = dibujo.color;
        color.a = alfa;
        dibujo.color = color;
    }

    public bool Escondido()
    {
        return escondido;
    }

    public void Recoger(int cantidad)
    {
        monedas = monedas + cantidad;
        juntadas = juntadas + cantidad;
        Debug.Log("El goblin tiene " + monedas + " monedas");
        contador.Mostrar(monedas);
    }

    public int Monedas()
    {
        return monedas;
    }

    public void PerderMonedas()
    {
        int cuantas = Mathf.Min(perdidaPorGolpe, monedas);
        monedas = monedas - cuantas;
        Debug.Log("El goblin suelta " + cuantas + " monedas");
        contador.Mostrar(monedas);

        for (int i = 0; i < cuantas; i++)
        {
            Vector2 lugar = (Vector2)transform.position + Random.insideUnitCircle.normalized * distancia;
            Instantiate(moneda, lugar, Quaternion.identity);
        }
    }

    public void Morir()
    {
        Debug.Log("El goblin cae. La arena vuelve a empezar");
        GuardarRecord();
        GetComponent<Combatiente>().enabled = false;
        cuerpo.linearVelocity = Vector2.zero;
        animador.SetBool("Corriendo", false);
        StartCoroutine(Reiniciar());
        enabled = false;
    }

    void GuardarRecord()
    {
        int record = PlayerPrefs.GetInt("RecordMonedas", 0);
        if (juntadas > record)
        {
            PlayerPrefs.SetInt("RecordMonedas", juntadas);
            Debug.Log("¡Récord nuevo! " + juntadas + " monedas");
        }
        else
        {
            Debug.Log("Juntaste " + juntadas + " monedas. El récord sigue en " + record);
        }
    }

    IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(espera);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
