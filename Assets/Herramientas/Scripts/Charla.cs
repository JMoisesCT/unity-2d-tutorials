using UnityEngine;
using UnityEngine.InputSystem;

public class Charla : MonoBehaviour
{
    [Header("Lo que dice")]
    [Tooltip("La charla de la primera vez")]
    [SerializeField] Dialogo primera;
    [Tooltip("Lo que dice si le vuelves a hablar")]
    [SerializeField] Dialogo otraVez;

    [Header("Piezas de la escena")]
    [Tooltip("El globo con el «!» que aparece cuando la rana está cerca")]
    [SerializeField] GameObject aviso;
    [SerializeField] CajaDialogo caja;

    InputAction hablar;
    bool cerca = false;
    bool yaHablaron = false;

    void Start()
    {
        aviso.SetActive(false);
        hablar = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        aviso.SetActive(cerca && !caja.Abierta());

        if (cerca && hablar.WasPressedThisFrame())
        {
            if (caja.Abierta())
            {
                caja.Siguiente();
            }
            else if (yaHablaron)
            {
                caja.Empezar(otraVez);
            }
            else
            {
                caja.Empezar(primera);
                yaHablaron = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            cerca = true;
        }
    }

    void OnTriggerExit2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            cerca = false;
        }
    }
}
