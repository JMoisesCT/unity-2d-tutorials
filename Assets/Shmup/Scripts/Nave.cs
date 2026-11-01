using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Nave : MonoBehaviour
{
    [Header("Ficha")]
    [Tooltip("Cómo se llama esta nave")]
    [SerializeField] string nombre = "Nave azul";
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 6f;

    [Header("Bordes")]
    [Tooltip("Media nave: cuánto se queda adentro el centro, en X y en Y")]
    [SerializeField] Vector2 margen = new Vector2(0.4f, 0.5f);

    [Header("Disparo")]
    [Tooltip("El prefab que sale al disparar")]
    [SerializeField] Laser laser;
    [Tooltip("Un grupo de cañones por nivel: Nivel1, Nivel2, Nivel3")]
    [SerializeField] Transform[] niveles;
    [Tooltip("Qué grupo dispara (de 1 a 3)")]
    [SerializeField] int nivel = 1;
    [Tooltip("Segundos entre un disparo y el siguiente")]
    [SerializeField] float cadencia = 0.2f;
    [Tooltip("El sonido de cada disparo")]
    [SerializeField] AudioClip sonidoLaser;
    [Tooltip("Dónde se guardan los láseres en la Hierarchy")]
    [SerializeField] Transform carpeta;

    [Header("Daño")]
    [Tooltip("Golpes que aguanta")]
    [SerializeField] int vidas = 3;
    [Tooltip("Cuánta vida le quita a lo que choca con ella")]
    [SerializeField] int dañoChoque = 3;
    [Tooltip("Segundos sin recibir daño después de un golpe")]
    [SerializeField] float invulnerable = 2f;
    [Tooltip("Segundos entre un parpadeo y el siguiente")]
    [SerializeField] float parpadeo = 0.1f;

    [Header("Escudo")]
    [Tooltip("El hijo con el dibujo del escudo (empieza apagado)")]
    [SerializeField] GameObject escudo;
    [Tooltip("El sonido al ponerse el escudo")]
    [SerializeField] AudioClip sonidoEscudo;
    [Tooltip("El sonido cuando el escudo se rompe")]
    [SerializeField] AudioClip sonidoRomper;

    [Header("Al perder")]
    [Tooltip("El prefab que aparece en su lugar")]
    [SerializeField] GameObject explosion;
    [Tooltip("El sonido de perder")]
    [SerializeField] AudioClip sonidoPerder;
    [Tooltip("Segundos hasta empezar de nuevo")]
    [SerializeField] float espera = 2f;

    InputAction mover;
    InputAction disparar;
    Rigidbody2D cuerpo;
    Vector2 direccion;
    Vector2 minimo;
    Vector2 maximo;
    float siguienteDisparo;
    AudioSource altavoz;
    int creados;
    ObjectPool<Laser> cargador;
    SpriteRenderer dibujo;
    bool protegida;

    void Start()
    {
        Debug.Log(nombre + " lista para despegar. Velocidad: " + velocidad);
        mover = InputSystem.actions.FindAction("Move");
        disparar = InputSystem.actions.FindAction("Attack");
        cuerpo = GetComponent<Rigidbody2D>();
        altavoz = GetComponent<AudioSource>();
        dibujo = GetComponent<SpriteRenderer>();
        minimo = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maximo = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Debug.Log("Pantalla: de " + minimo + " a " + maximo);
        cargador = new ObjectPool<Laser>(
            () =>
            {
                creados++;
                Debug.Log("Láseres creados: " + creados);
                return Instantiate(laser, carpeta);
            },
            l => l.gameObject.SetActive(true),
            l => l.gameObject.SetActive(false));
    }

    void Update()
    {
        direccion = mover.ReadValue<Vector2>();
        if (disparar.IsPressed() && Time.time >= siguienteDisparo)
        {
            foreach (Transform canon in niveles[nivel - 1])
            {
                Laser nuevo = cargador.Get();
                nuevo.transform.SetPositionAndRotation(canon.position, canon.rotation);
                nuevo.UsarCargador(cargador);
            }
            siguienteDisparo = Time.time + cadencia;
            altavoz.PlayOneShot(sonidoLaser);
        }
    }

    public void MejorarDisparo()
    {
        if (nivel < niveles.Length)
        {
            nivel++;
        }
        Debug.Log("Disparo nivel " + nivel);
    }

    void FixedUpdate()
    {
        Vector2 siguiente = cuerpo.position + direccion * velocidad * Time.deltaTime;
        siguiente.x = Mathf.Clamp(siguiente.x, minimo.x + margen.x, maximo.x - margen.x);
        siguiente.y = Mathf.Clamp(siguiente.y, minimo.y + margen.y, maximo.y - margen.y);
        cuerpo.MovePosition(siguiente);
    }

    public void PonerEscudo()
    {
        escudo.SetActive(true);
        altavoz.PlayOneShot(sonidoEscudo);
        Debug.Log("Escudo puesto");
    }

    public void RecibirGolpe()
    {
        if (protegida)
        {
            return;
        }
        if (escudo.activeSelf)
        {
            escudo.SetActive(false);
            altavoz.PlayOneShot(sonidoRomper);
            Debug.Log("El escudo aguantó el golpe");
            StartCoroutine(Parpadear());
            return;
        }
        vidas--;
        Debug.Log("¡Golpe! Vidas: " + vidas);
        if (vidas <= 0)
        {
            Morir();
            return;
        }
        StartCoroutine(Parpadear());
    }

    void Morir()
    {
        Debug.Log(nombre + " fue destruida");
        Instantiate(explosion, transform.position, Quaternion.identity);
        altavoz.PlayOneShot(sonidoPerder);
        dibujo.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
        StartCoroutine(Reiniciar());
    }

    IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(espera);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator Parpadear()
    {
        protegida = true;
        float hasta = Time.time + invulnerable;
        while (Time.time < hasta)
        {
            dibujo.enabled = !dibujo.enabled;
            yield return new WaitForSeconds(parpadeo);
        }
        dibujo.enabled = true;
        protegida = false;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.TryGetComponent(out Vida vida))
        {
            vida.RecibirDaño(dañoChoque);
            RecibirGolpe();
        }
    }
}
