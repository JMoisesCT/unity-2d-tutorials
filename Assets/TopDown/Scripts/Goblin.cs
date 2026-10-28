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
    Rigidbody2D cuerpo;
    SpriteRenderer dibujo;
    Animator animador;
    Vector2 direccion;
    int monedas;

    void Start()
    {
        mover = InputSystem.actions.FindAction("Move");
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        GetComponent<Vida>().Empezar(vidaMaxima);
    }

    void Update()
    {
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

    public void Recoger(int cantidad)
    {
        monedas = monedas + cantidad;
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
        GetComponent<Combatiente>().enabled = false;
        cuerpo.linearVelocity = Vector2.zero;
        animador.SetBool("Corriendo", false);
        StartCoroutine(Reiniciar());
        enabled = false;
    }

    IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(espera);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
