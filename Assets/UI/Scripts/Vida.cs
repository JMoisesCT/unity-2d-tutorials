using System;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [Tooltip("Con cuánta vida empieza.")]
    [SerializeField] int maxima = 5;

    int actual;

    public event Action<int, int> Cambio;

    void Start()
    {
        actual = maxima;
        Cambio?.Invoke(actual, maxima);
    }

    public void RecibirDaño(int daño)
    {
        actual = Mathf.Max(actual - daño, 0);
        Cambio?.Invoke(actual, maxima);
    }
}
