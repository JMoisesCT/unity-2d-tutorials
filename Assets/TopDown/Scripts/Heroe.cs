using UnityEngine;
using UnityEngine.InputSystem;

public class Heroe : MonoBehaviour
{
    [Header("Ficha")]
    [SerializeField] string nombre = "Caballero";
    [SerializeField] float velocidad = 4.5f;

    [Header("Ataque")]
    [Tooltip("Se voltea con el héroe: lleva la espada")]
    [SerializeField] Transform mano;
    [Tooltip("El Animator de la espada")]
    [SerializeField] Animator espada;
    [Tooltip("Centro del círculo del golpe")]
    [SerializeField] Transform puntoGolpe;
    [Tooltip("Radio del círculo del golpe")]
    [SerializeField] float alcance = 0.6f;
    [Tooltip("Vida que quita cada golpe")]
    [SerializeField] int daño = 1;

    InputAction mover;
    InputAction atacar;
    Rigidbody2D cuerpo;
    Vector2 direccion;
    SpriteRenderer dibujo;
    Animator animador;

    void Start()
    {
        Debug.Log(nombre + " entra en la mazmorra. Velocidad: " + velocidad);
        mover = InputSystem.actions.FindAction("Move");
        atacar = InputSystem.actions.FindAction("Attack");
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
            mano.localScale = new Vector3(-1, 1, 1);
        }
        else if (direccion.x > 0)
        {
            dibujo.flipX = false;
            mano.localScale = new Vector3(1, 1, 1);
        }

        bool seMueve = direccion.x != 0 || direccion.y != 0;
        animador.SetBool("Corriendo", seMueve);

        if (atacar.WasPressedThisFrame())
        {
            Atacar();
        }
    }

    void FixedUpdate()
    {
        cuerpo.linearVelocity = direccion * velocidad;
    }

    void Atacar()
    {
        espada.SetTrigger("Atacar");

        Collider2D[] tocados = Physics2D.OverlapCircleAll(puntoGolpe.position, alcance);
        foreach (Collider2D tocado in tocados)
        {
            if (tocado.gameObject != gameObject && tocado.TryGetComponent(out Vida vida))
            {
                vida.RecibirDaño(daño);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(puntoGolpe.position, alcance);
    }
}
