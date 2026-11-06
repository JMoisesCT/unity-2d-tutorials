using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarcoFoco : MonoBehaviour
{
    [Tooltip("Cuántas unidades sobran alrededor de lo seleccionado.")]
    [SerializeField] float margen = 24f;
    [Tooltip("Qué tan rápido llega: más alto, más rápido.")]
    [SerializeField] float rapidez = 18f;

    RectTransform marco;
    Image dibujo;

    void Awake()
    {
        marco = GetComponent<RectTransform>();
        dibujo = GetComponent<Image>();
    }

    void LateUpdate()
    {
        GameObject foco = EventSystem.current.currentSelectedGameObject;
        dibujo.enabled = foco != null;
        if (foco == null)
        {
            return;
        }

        RectTransform objetivo = foco.GetComponent<RectTransform>();
        float escala = objetivo.lossyScale.x / marco.lossyScale.x;
        Vector3 centro = objetivo.TransformPoint(objetivo.rect.center);
        Vector2 tamaño = objetivo.rect.size * escala + Vector2.one * margen;

        float paso = rapidez * Time.unscaledDeltaTime;
        marco.position = Vector3.Lerp(marco.position, centro, paso);
        marco.sizeDelta = Vector2.Lerp(marco.sizeDelta, tamaño, paso);
    }
}
