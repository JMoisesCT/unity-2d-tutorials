using UnityEngine;
using UnityEngine.InputSystem;

public class Pegajoso : Personaje
{
    [Header("Gravedad propia")]
    [Tooltip("Cuánto lo aprieta contra la superficie que pisa")]
    [SerializeField] float fuerzaPegado = 30f;
    [Tooltip("Cuánto lo empuja hacia abajo en el aire")]
    [SerializeField] float gravedad = 10f;

    [Header("Saltar")]
    [Tooltip("La fuerza del salto que lo despega de la superficie")]
    [SerializeField] float fuerzaSalto = 5f;

    [Header("Dibujo")]
    [Tooltip("Pegado a una superficie")]
    [SerializeField] Sprite caminando;
    [Tooltip("En el aire, escondido en el caparazón")]
    [SerializeField] Sprite caparazon;

    SpriteRenderer dibujo;
    InputAction saltar;

    protected override void Start()
    {
        base.Start();
        cuerpo.gravityScale = 0f;
        saltar = InputSystem.actions.FindAction("Jump");
        dibujo = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (estado == Estado.Aire)
        {
            cuerpo.AddForce(Vector2.down * gravedad);
        }
        else
        {
            cuerpo.AddForce(-transform.up * fuerzaPegado);
        }
    }

    void LateUpdate()
    {
        if (estado == Estado.Suelo && saltar.WasPressedThisFrame())
        {
            estado = Estado.Aire;
            cuerpo.AddForce(transform.up * fuerzaSalto, ForceMode2D.Impulse);
        }

        float direccion = mover.ReadValue<Vector2>().x;
        if (direccion > 0f)
        {
            dibujo.flipX = true;
        }
        if (direccion < 0f)
        {
            dibujo.flipX = false;
        }

        dibujo.sprite = caminando;
        if (estado == Estado.Aire)
        {
            dibujo.sprite = caparazon;
        }
    }

    protected override void AplicarPendiente()
    {
        // El caracol no resbala: la cuesta no cambia su velocidad
    }

    protected override bool SeCaeDeLaPared()
    {
        // El caracol no se cae nunca: va pegado
        return false;
    }
}
