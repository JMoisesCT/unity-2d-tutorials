using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ventana : MonoBehaviour
{
    [Tooltip("Lo que queda seleccionado al abrir.")]
    [SerializeField] GameObject primero;

    Animator animador;
    CanvasGroup grupo;

    public event Action AlAbrir;

    void Awake()
    {
        animador = GetComponent<Animator>();
        grupo = GetComponent<CanvasGroup>();
        grupo.interactable = false;
    }

    public void Abrir()
    {
        animador.SetBool("Abierta", true);
        grupo.interactable = true;
        EventSystem.current.SetSelectedGameObject(primero);
        AlAbrir?.Invoke();
    }

    public void Cerrar()
    {
        animador.SetBool("Abierta", false);
        grupo.interactable = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    public CanvasGroup Grupo()
    {
        return grupo;
    }
}
