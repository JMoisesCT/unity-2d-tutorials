using System;
using UnityEngine;

public class Ventana : MonoBehaviour
{
    Animator animador;

    public event Action AlAbrir;

    void Awake()
    {
        animador = GetComponent<Animator>();
    }

    public void Abrir()
    {
        animador.SetBool("Abierta", true);
        AlAbrir?.Invoke();
    }

    public void Cerrar()
    {
        animador.SetBool("Abierta", false);
    }
}
