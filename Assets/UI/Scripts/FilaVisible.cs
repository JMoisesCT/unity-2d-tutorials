using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FilaVisible : MonoBehaviour, ISelectHandler
{
    [Tooltip("Cuánto aire deja arriba y abajo de la fila.")]
    [SerializeField] float margen = 16f;

    public void OnSelect(BaseEventData datos)
    {
        ScrollRect lista = GetComponentInParent<ScrollRect>();
        RectTransform fila = GetComponent<RectTransform>();

        float arriba = -fila.localPosition.y - fila.rect.height / 2f - margen;
        float abajo = -fila.localPosition.y + fila.rect.height / 2f + margen;
        float alto = lista.viewport.rect.height;
        float mirando = lista.content.anchoredPosition.y;

        if (arriba < mirando)
        {
            mirando = arriba;
        }
        else if (abajo > mirando + alto)
        {
            mirando = abajo - alto;
        }
        lista.content.anchoredPosition = new Vector2(0f, mirando);
    }
}
