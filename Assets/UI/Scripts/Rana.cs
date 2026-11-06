using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rana : MonoBehaviour
{
    [SerializeField] float velocidad = 2.5f;
    [Header("Interfaz")]
    [Tooltip("La barra del HUD que muestra las frutas.")]
    [SerializeField] BarraFrutas barra;
    [Tooltip("Cuántas frutas hay en el nivel.")]
    [SerializeField] int frutasTotales = 5;
    [Tooltip("Cuántos puntos vale cada fruta.")]
    [SerializeField] int puntosPorFruta = 100;
    int frutas = 0;

    public event Action<Vector3, int> FrutaJuntada;
    public event Action TodasJuntadas;

    InputAction mover;
    SpriteRenderer dibujo;
    Animator animador;
    Vida vida;

    void Start()
    {
        mover = InputSystem.actions.FindAction("Move");
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        vida = GetComponent<Vida>();
        barra.Mostrar(frutas, frutasTotales);
    }

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

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
            FrutaJuntada?.Invoke(otro.transform.position, puntosPorFruta);
            if (frutas == frutasTotales)
            {
                TodasJuntadas?.Invoke();
            }
            Destroy(otro.gameObject);
        }
        else if (otro.CompareTag("Trampa"))
        {
            vida.RecibirDaño(1);
        }
    }
}
