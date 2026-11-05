using UnityEngine;

public class Ventana : MonoBehaviour
{
    Animator animador;

    void Awake()
    {
        animador = GetComponent<Animator>();
    }

    public void Abrir()
    {
        animador.SetBool("Abierta", true);
    }

    public void Cerrar()
    {
        animador.SetBool("Abierta", false);
    }
}
