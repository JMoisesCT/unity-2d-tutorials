using UnityEngine;
using TMPro;

public class Marcador : MonoBehaviour
{
    TMP_Text texto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texto = GetComponent<TMP_Text>();
        Mostrar(0);
    }

    public void Mostrar(int cantidad)
    {
        texto.text = "Frutas: " + cantidad;
    }
}