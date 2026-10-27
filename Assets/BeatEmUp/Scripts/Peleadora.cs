using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Peleadora : MonoBehaviour
{
    [Header("Peleadora")]
    [SerializeField] string nombre = "Brawler Girl";

    [Header("Movimiento")]
    [Tooltip("Unidades por segundo hacia la izquierda o la derecha")]
    [SerializeField] float velocidad = 4.5f;
    [Tooltip("Unidades por segundo hacia el fondo o hacia el frente")]
    [SerializeField] float velocidadProfundidad = 2.5f;

    [Header("Límites de la calle")]
    [SerializeField] float izquierda = -11f;
    [SerializeField] float derecha = 11f;
    [SerializeField] float arriba = 0.5f;
    [SerializeField] float abajo = -6.5f;
    [Tooltip("La cámara que avanza: la chica no puede volver más atrás que su borde")]
    [SerializeField] CamaraCalle camara;

    [Header("Obstáculos")]
    [Tooltip("Con qué choca: con Use Triggers apagado, los triggers no la frenan")]
    [SerializeField] ContactFilter2D filtro;

    [Header("Dibujo")]
    [Tooltip("El Sprite Renderer del hijo Cuerpo")]
    [SerializeField] SpriteRenderer dibujo;
    [Tooltip("El Animator del hijo Cuerpo")]
    [SerializeField] Animator animador;

    [Header("Golpes recibidos")]
    [Tooltip("Segundos que queda quieta al recibir un golpe")]
    [SerializeField] float aturdida = 0.4f;
    [Tooltip("Segundos quieta antes de volver a empezar cuando se queda sin vida")]
    [SerializeField] float reinicio = 1.5f;

    InputAction mover;
    Rigidbody2D cuerpo;
    Vector2 direccion;
    Salto salto;
    Combo combo;
    Vida vida;
    float quietaHasta;
    RaycastHit2D[] choques = new RaycastHit2D[4];

    void Start()
    {
        Debug.Log(nombre + " llega a la calle. Velocidad: " + velocidad);
        mover = InputSystem.actions.FindAction("Move");
        cuerpo = GetComponent<Rigidbody2D>();
        salto = GetComponent<Salto>();
        combo = GetComponent<Combo>();
        vida = GetComponent<Vida>();
    }

    void Update()
    {
        direccion = mover.ReadValue<Vector2>();
        if (salto.EnElAire())
        {
            direccion.y = 0;
        }

        if (combo.Golpeando() || Time.time < quietaHasta || !vida.Vivo())
        {
            direccion = Vector2.zero;
        }

        if (direccion.x < 0)
        {
            dibujo.flipX = true;
        }
        else if (direccion.x > 0)
        {
            dibujo.flipX = false;
        }

        bool camina = direccion.x != 0 || direccion.y != 0;
        animador.SetBool("Caminando", camina);
        animador.SetBool("EnElAire", salto.EnElAire());
    }

    void FixedUpdate()
    {
        Vector2 paso = new Vector2(direccion.x * velocidad, direccion.y * velocidadProfundidad);
        Vector2 avance = paso * Time.deltaTime;
        if (Choca(new Vector2(avance.x, 0)))
        {
            avance.x = 0;
        }
        if (Choca(new Vector2(0, avance.y)))
        {
            avance.y = 0;
        }
        Vector2 siguiente = cuerpo.position + avance;
        float borde = Mathf.Max(izquierda, camara.BordeIzquierdo());
        siguiente.x = Mathf.Clamp(siguiente.x, borde, derecha);
        siguiente.y = Mathf.Clamp(siguiente.y, abajo, arriba);
        cuerpo.MovePosition(siguiente);
    }

    bool Choca(Vector2 avance)
    {
        return cuerpo.Cast(avance, filtro, choques, avance.magnitude) > 0;
    }

    public void Recibir(int daño)
    {
        if (!vida.RecibirDaño(daño))
        {
            return;
        }
        animador.SetTrigger("Herido");
        quietaHasta = Time.time + aturdida;
        StartCoroutine(Parpadear());
        if (!vida.Vivo())
        {
            Debug.Log(nombre + " ya no puede más");
            combo.enabled = false;
            salto.enabled = false;
            StartCoroutine(Reiniciar());
        }
    }

    IEnumerator Parpadear()
    {
        while (vida.Protegida())
        {
            dibujo.enabled = false;
            yield return new WaitForSeconds(0.1f);
            dibujo.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(reinicio);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
