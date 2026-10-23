using UnityEngine;
using UnityEngine.InputSystem;

public class Heroe : MonoBehaviour
{
    [Header("Ficha")]
    [Tooltip("Los números del héroe: un asset de DatosHeroe")]
    [SerializeField] DatosHeroe datos;

    [Header("Ataque")]
    [Tooltip("Se voltea con el héroe: lleva la espada")]
    [SerializeField] Transform mano;
    [Tooltip("El Animator de la espada")]
    [SerializeField] Animator espada;
    [Tooltip("Centro del círculo del golpe")]
    [SerializeField] Transform puntoGolpe;

    InputAction mover;
    InputAction atacar;
    Rigidbody2D cuerpo;
    Vector2 direccion;
    SpriteRenderer dibujo;
    Animator animador;
    float siguienteGolpe;

    void Start()
    {
        Debug.Log(datos.nombre + " entra en la mazmorra. Velocidad: " + datos.velocidad);
        mover = InputSystem.actions.FindAction("Move");
        atacar = InputSystem.actions.FindAction("Attack");
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        GetComponent<Vida>().Empezar(datos.vida);
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

        if (atacar.WasPressedThisFrame() && Time.time >= siguienteGolpe)
        {
            Atacar();
        }
    }

    void FixedUpdate()
    {
        cuerpo.linearVelocity = direccion * datos.velocidad;
    }

    void Atacar()
    {
        siguienteGolpe = Time.time + datos.cadencia;
        espada.SetTrigger("Atacar");

        Collider2D[] tocados = Physics2D.OverlapCircleAll(puntoGolpe.position, datos.alcance);
        foreach (Collider2D tocado in tocados)
        {
            if (tocado.gameObject != gameObject && tocado.TryGetComponent(out Vida vida))
            {
                vida.RecibirDaño(datos.daño);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(puntoGolpe.position, datos.alcance);
    }
}
