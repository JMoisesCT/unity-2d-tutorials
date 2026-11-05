using TMPro;
using UnityEngine;

public class Puntos : MonoBehaviour
{
    [Tooltip("El texto que muestra los puntos.")]
    [SerializeField] TMP_Text texto;

    public void Mostrar(int puntos)
    {
        texto.text = puntos.ToString("00000");
    }
}
