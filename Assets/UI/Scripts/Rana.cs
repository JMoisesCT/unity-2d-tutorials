using UnityEngine;
using UnityEngine.InputSystem;

public class Rana : MonoBehaviour
{
    [SerializeField] float velocidad = 2.5f;
    [Header("Interfaz")]
    [Tooltip("La barra del HUD que muestra las frutas.")]
    [SerializeField] BarraFrutas barra;
    [Tooltip("El cartel que aparece al juntarlas todas.")]
    [SerializeField] Cartel cartel;
    [Tooltip("Cuántas frutas hay en el nivel.")]
    [SerializeField] int frutasTotales = 5;
    int frutas = 0;
    InputAction mover;
    SpriteRenderer dibujo;
    Animator animador;

    void Start()
    {
        mover = InputSystem.actions.FindAction("Move");
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        barra.Mostrar(frutas, frutasTotales);
    }

    void Update()
    {
        Vector2 direccion = mover.ReadValue<Vector2>();
        transform.Translate(direccion * velocidad * Time.deltaTime);

        if (direccion.x < 0)
        {
            dibujo.flipX = true;
        }
        else if (direccion.x > 0)
        {
            dibujo.flipX = false;
        }
        animador.SetBool("Corriendo", direccion != Vector2.zero);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Fruta"))
        {
            frutas = frutas + 1;
            barra.Mostrar(frutas, frutasTotales);
            if (frutas == frutasTotales)
            {
                cartel.Mostrar();
            }
            Destroy(otro.gameObject);
        }
    }
}
