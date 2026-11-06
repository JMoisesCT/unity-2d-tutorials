using UnityEngine;
using UnityEngine.InputSystem;

public class Partida : MonoBehaviour
{
    enum Estado { Jugando, Pausada, Terminada }

    [Header("Ventanas")]
    [Tooltip("La ventana que se abre al pausar.")]
    [SerializeField] Ventana ventanaPausa;
    [Tooltip("La ventana que se abre al perder.")]
    [SerializeField] Ventana ventanaGameOver;
    [Tooltip("La ventana con las estrellas.")]
    [SerializeField] Completo ventanaCompleto;
    [Header("Partida")]
    [Tooltip("La vida de la rana.")]
    [SerializeField] Vida vida;
    [Tooltip("La rana que avisa cuando junta todas las frutas.")]
    [SerializeField] Rana rana;
    [Tooltip("Segundos para ganar la estrella de tiempo.")]
    [SerializeField] float tiempoEstrella = 10f;

    Estado estado = Estado.Jugando;
    bool sinGolpes = true;
    InputAction atras;

    void Start()
    {
        atras = InputSystem.actions.FindAction("UI/Cancel");
    }

    void OnEnable()
    {
        vida.Cambio += RevisarVida;
        rana.TodasJuntadas += Ganar;
    }

    void OnDisable()
    {
        vida.Cambio -= RevisarVida;
        rana.TodasJuntadas -= Ganar;
    }

    void Update()
    {
        if (atras.WasPressedThisFrame())
        {
            if (estado == Estado.Jugando)
            {
                Pausar();
            }
            else if (estado == Estado.Pausada)
            {
                Continuar();
            }
        }
    }

    public void Pausar()
    {
        estado = Estado.Pausada;
        Time.timeScale = 0f;
        ventanaPausa.Abrir();
    }

    public void Continuar()
    {
        estado = Estado.Jugando;
        Time.timeScale = 1f;
        ventanaPausa.Cerrar();
    }

    void RevisarVida(int valor, int maxima)
    {
        if (valor < maxima)
        {
            sinGolpes = false;
        }
        if (valor == 0)
        {
            estado = Estado.Terminada;
            Time.timeScale = 0f;
            ventanaGameOver.Abrir();
        }
    }

    void Ganar()
    {
        estado = Estado.Terminada;
        Time.timeScale = 0f;
        float tiempo = Time.timeSinceLevelLoad;
        ventanaCompleto.Mostrar(tiempo, ContarEstrellas(tiempo));
    }

    int ContarEstrellas(float tiempo)
    {
        int estrellas = 1;
        if (sinGolpes)
        {
            estrellas = estrellas + 1;
        }
        if (tiempo < tiempoEstrella)
        {
            estrellas = estrellas + 1;
        }
        return estrellas;
    }
}
