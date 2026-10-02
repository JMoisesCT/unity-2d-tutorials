using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class FichaRana : MonoBehaviour
{
    enum Estado { Jugando, Ganaste, GameOver }

    [SerializeField] string nombre = "Rana Ninja";
    [SerializeField] int vidas = 3;
    [SerializeField] float velocidad = 2.5f;
    [SerializeField] bool estaViva = true;
    [SerializeField] float borde = 8f;
    [SerializeField] int frutas = 0;
    [SerializeField] int meta = 10;
    [SerializeField] Marcador marcador;
    [SerializeField] TMP_Text mensaje;
    [SerializeField] Estado estado = Estado.Jugando;
    InputAction mover;
    SpriteRenderer dibujo;
    Animator animador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Me llamo " + nombre);
        Debug.Log("Vidas: " + vidas);
        Debug.Log("Velocidad: " + velocidad);
        Debug.Log("¿Está viva? " + estaViva);
        RecibirGolpe(1);
        mover = InputSystem.actions.FindAction("Move");
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direccion = mover.ReadValue<Vector2>();

        if (estado != Estado.Jugando)
        {
            direccion = new Vector2(0, 0);
        }

        if (direccion.x > 0 && transform.position.x > borde)
        {
            direccion.x = 0;
        }
        if (direccion.x < 0 && transform.position.x < -borde)
        {
            direccion.x = 0;
        }

        transform.Translate(direccion * velocidad * Time.deltaTime);

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

        if (estado != Estado.Jugando && Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene("MiPrimeraEscena");
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Fruta"))
        {
            SumarFruta();
            Destroy(otro.gameObject);
        }
        else if (otro.CompareTag("Trampa"))
        {
            RecibirGolpe(1);
        }
    }

    void SumarFruta()
    {
        frutas = frutas + 1;
        Debug.Log("Frutas: " + frutas);
        marcador.Mostrar(frutas);
        if (frutas == meta)
        {
            estado = Estado.Ganaste;
            mensaje.text = "¡GANASTE!\nPulsa R para jugar otra vez";
        }
    }

    void RecibirGolpe(int daño)
    {
        vidas = vidas - daño;
        Debug.Log("¡Auch! Le quedan " + vidas + " vidas.");
        if (SigueViva() == false)
        {
            estado = Estado.GameOver;
            mensaje.text = "GAME OVER\nPulsa R para jugar otra vez";
        }
    }

    bool SigueViva()
    {
        return estaViva && vidas > 0;
    }
}
