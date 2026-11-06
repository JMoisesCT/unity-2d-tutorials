using System;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [Tooltip("La vida máxima en fácil, normal y difícil.")]
    [SerializeField] int[] vidaPorDificultad = { 7, 5, 3 };

    int maxima;
    int actual;

    public event Action<int, int> Cambio;

    void Start()
    {
        int dificultad = PlayerPrefs.GetInt(Claves.Dificultad, 1);
        maxima = vidaPorDificultad[dificultad];
        actual = maxima;
        Cambio?.Invoke(actual, maxima);
    }

    public void RecibirDaño(int daño)
    {
        actual = Mathf.Max(actual - daño, 0);
        Cambio?.Invoke(actual, maxima);
    }
}
