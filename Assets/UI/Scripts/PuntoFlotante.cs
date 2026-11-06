using System.Collections;
using TMPro;
using UnityEngine;

public class PuntoFlotante : MonoBehaviour
{
    [Tooltip("El texto que muestra los puntos.")]
    [SerializeField] TMP_Text texto;
    [Tooltip("Cuánto sube, en unidades del Canvas.")]
    [SerializeField] float subida = 120f;
    [Tooltip("Cuántos segundos dura en pantalla.")]
    [SerializeField] float duracion = 0.8f;
    [Tooltip("El asset con las curvas de toda la interfaz.")]
    [SerializeField] Curvas curvas;

    public void Mostrar(int puntos)
    {
        texto.text = "+" + puntos;
        StartCoroutine(Flotar());
    }

    IEnumerator Flotar()
    {
        RectTransform caja = GetComponent<RectTransform>();
        Vector2 inicio = caja.anchoredPosition;
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            tiempo += Time.unscaledDeltaTime;
            float parte = tiempo / duracion;
            caja.anchoredPosition = inicio + Vector2.up * subida * curvas.suave.Evaluate(parte);
            texto.alpha = 1f - parte;
            yield return null;
        }
        Destroy(gameObject);
    }
}
