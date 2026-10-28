using UnityEngine;
using TMPro;

public class ContadorMonedas : MonoBehaviour
{
    [Tooltip("El número que se ve en pantalla")]
    [SerializeField] TMP_Text texto;

    public void Mostrar(int monedas)
    {
        texto.text = monedas.ToString();
    }
}
