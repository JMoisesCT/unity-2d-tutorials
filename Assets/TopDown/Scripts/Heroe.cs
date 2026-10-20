using UnityEngine;
using UnityEngine.InputSystem;

public class Heroe : MonoBehaviour
{
    [SerializeField] string nombre = "Caballero";
    [SerializeField] float velocidad = 4.5f;
    InputAction mover;
    Rigidbody2D cuerpo;
    Vector2 direccion;
    SpriteRenderer dibujo;
    Animator animador;

    void Start()
    {
        Debug.Log(nombre + " entra en la mazmorra. Velocidad: " + velocidad);
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

        bool seMueve = direccion.x != 0 || direccion.y != 0;
        animador.SetBool("Corriendo", seMueve);
    }

    void FixedUpdate()
    {
        cuerpo.linearVelocity = direccion * velocidad;
    }
}
