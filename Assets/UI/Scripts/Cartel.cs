using UnityEngine;

public class Cartel : MonoBehaviour
{
    [Tooltip("Dónde queda el cartel al aparecer.")]
    [SerializeField] Vector2 lugar = new Vector2(0, -150);

    public void Mostrar()
    {
        RectTransform caja = GetComponent<RectTransform>();
        caja.anchoredPosition = lugar;
    }
}
