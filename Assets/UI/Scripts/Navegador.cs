using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Navegador : MonoBehaviour
{
    [Tooltip("El Canvas Group del menú principal.")]
    [SerializeField] CanvasGroup principal;

    Stack<Ventana> abiertas = new Stack<Ventana>();
    Stack<GameObject> focos = new Stack<GameObject>();
    InputAction atras;

    void Start()
    {
        atras = InputSystem.actions.FindAction("UI/Cancel");
    }

    public void Abrir(Ventana ventana)
    {
        focos.Push(EventSystem.current.currentSelectedGameObject);
        Arriba().interactable = false;
        ventana.Abrir();
        abiertas.Push(ventana);
    }

    public void Atras()
    {
        if (abiertas.Count == 0)
        {
            return;
        }
        Ventana ultima = abiertas.Pop();
        ultima.Cerrar();
        Arriba().interactable = true;
        EventSystem.current.SetSelectedGameObject(focos.Pop());
    }

    CanvasGroup Arriba()
    {
        if (abiertas.Count == 0)
        {
            return principal;
        }
        return abiertas.Peek().Grupo();
    }

    bool ListaAbierta()
    {
        GameObject foco = EventSystem.current.currentSelectedGameObject;
        if (foco == null)
        {
            return false;
        }
        TMP_Dropdown lista = foco.GetComponentInParent<TMP_Dropdown>();
        return lista != null && lista.IsExpanded;
    }

    void Update()
    {
        if (atras.WasPressedThisFrame() && !ListaAbierta())
        {
            Atras();
        }
    }
}
