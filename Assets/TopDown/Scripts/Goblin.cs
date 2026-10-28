using UnityEngine;
using UnityEngine.InputSystem;

public class Goblin : MonoBehaviour
{
    [Header("Movimiento")]
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 5f;

    [Header("Monedas")]
    [Tooltip("El contador del HUD")]
    [SerializeField] ContadorMonedas contador;

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
}
