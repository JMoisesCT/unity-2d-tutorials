using UnityEngine;
using UnityEngine.InputSystem;

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

    InputAction mover;
    Rigidbody2D cuerpo;
    Vector2 direccion;
    SpriteRenderer dibujo;
    Animator animador;

    void Start()
    {
        Debug.Log(nombre + " llega a la calle. Velocidad: " + velocidad);
        mover = InputSystem.actions.FindAction("Move");
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    void Update()
    {
        direccion = mover.ReadValue<Vector2>();

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
    }

    void FixedUpdate()
    {
        Vector2 paso = new Vector2(direccion.x * velocidad, direccion.y * velocidadProfundidad);
        Vector2 siguiente = cuerpo.position + paso * Time.deltaTime;
        siguiente.x = Mathf.Clamp(siguiente.x, izquierda, derecha);
        siguiente.y = Mathf.Clamp(siguiente.y, abajo, arriba);
        cuerpo.MovePosition(siguiente);
    }
}
