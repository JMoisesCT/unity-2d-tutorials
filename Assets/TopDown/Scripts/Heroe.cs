using UnityEngine;
using UnityEngine.InputSystem;

public class Heroe : MonoBehaviour
{
    [Header("Ficha")]
    [Tooltip("Los números del héroe: un asset de DatosHeroe")]
    [SerializeField] DatosHeroe datos;

    [Header("Ataque")]
    [Tooltip("Se voltea con el héroe: ahí aparece el arma")]
    [SerializeField] Transform mano;

    InputAction mover;
    InputAction atacar;
    Rigidbody2D cuerpo;
    Vector2 direccion;
    SpriteRenderer dibujo;
    Animator animador;
    float siguienteGolpe;
    Ataque arma;

    void Start()
    {
        Debug.Log(datos.nombre + " entra en la mazmorra. Velocidad: " + datos.velocidad);
        mover = InputSystem.actions.FindAction("Move");
        atacar = InputSystem.actions.FindAction("Attack");
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        animador.runtimeAnimatorController = datos.animaciones;
        GetComponent<Vida>().Empezar(datos.vida);
        Equipar();
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

    public void Transformar(DatosHeroe nueva)
    {
        Debug.Log(name + " se transforma en " + nueva.nombre);
        datos = nueva;
        animador.runtimeAnimatorController = datos.animaciones;
        GetComponent<Vida>().CambiarMaxima(datos.vida);
        Equipar();
    }

    void Equipar()
    {
        if (arma != null)
        {
            Destroy(arma.gameObject);
        }
        arma = Instantiate(datos.arma, mano);
    }

    void Atacar()
    {
        siguienteGolpe = Time.time + datos.cadencia;
        arma.Atacar(datos);
    }
}
