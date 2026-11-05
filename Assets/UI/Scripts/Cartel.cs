using TMPro;
using UnityEngine;

public class Cartel : MonoBehaviour
{
    [Tooltip("Dónde queda el cartel al aparecer.")]
    [SerializeField] Vector2 lugar = new Vector2(0, -150);
    [Tooltip("El texto que está dentro del cartel.")]
    [SerializeField] TMP_Text texto;

    public void Mostrar(float tiempo)
    {
        int total = Mathf.FloorToInt(tiempo);
        int minutos = total / 60;
        int segundos = total % 60;
        texto.text = $"¡Nivel completo!\nTiempo: <color=#A8471A>{minutos:00}:{segundos:00}</color>";

        RectTransform caja = GetComponent<RectTransform>();
        caja.anchoredPosition = lugar;
    }
}
