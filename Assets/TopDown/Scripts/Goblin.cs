using UnityEngine;
using UnityEngine.InputSystem;

public class Goblin : MonoBehaviour
{
    [Header("Movimiento")]
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 5f;

    InputAction mover;
    Rigidbody2D cuerpo;
    SpriteRenderer dibujo;
    Animator animador;
    Vector2 direccion;

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
}
