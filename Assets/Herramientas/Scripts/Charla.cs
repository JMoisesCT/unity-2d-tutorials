using UnityEngine;
using UnityEngine.InputSystem;

public class Charla : MonoBehaviour
{
    [Header("Lo que dice")]
    [SerializeField] string nombre = "Mask Dude";
    [TextArea(2, 3)]
    [SerializeField] string[] frases;

    [Header("Piezas de la escena")]
    [Tooltip("El globo con el «!» que aparece cuando la rana está cerca")]
    [SerializeField] GameObject aviso;
    [SerializeField] CajaDialogo caja;

    InputAction hablar;
    bool cerca = false;

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
            else
            {
                caja.Empezar(nombre, frases);
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
