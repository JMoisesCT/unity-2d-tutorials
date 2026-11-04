using UnityEngine;

public class BarraFrutas : MonoBehaviour
{
    [Header("Relleno")]
    [Tooltip("La caja que crece dentro de la barra.")]
    [SerializeField] RectTransform relleno;
    [Tooltip("Cuánto crece por cada fruta, en píxeles del Canvas.")]
    [SerializeField] float anchoPorFruta = 100f;

    public void Mostrar(int frutas)
    {
        relleno.sizeDelta = new Vector2(frutas * anchoPorFruta, relleno.sizeDelta.y);
    }
}
