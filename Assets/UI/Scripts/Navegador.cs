using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Navegador : MonoBehaviour
{
    Stack<Ventana> abiertas = new Stack<Ventana>();

    public void Abrir(Ventana ventana)
    {
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
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Atras();
        }
    }
}
