using System.Collections;
using TMPro;
using UnityEngine;

public class Puntos : MonoBehaviour
{
    [Tooltip("El texto que muestra los puntos.")]
    [SerializeField] TMP_Text texto;
    [Tooltip("La rana que avisa cuando junta una fruta.")]
    [SerializeField] Rana rana;
    [Tooltip("Cuántos segundos tarda en subir el número.")]
    [SerializeField] float duracion = 0.4f;
    [Header("Rebote")]
    [Tooltip("Cuánto crece el número al sumar.")]
    [SerializeField] float golpe = 1.3f;
    [Tooltip("El asset con las curvas de toda la interfaz.")]
    [SerializeField] Curvas curvas;

    int total = 0;
    int mostrado = 0;
    Coroutine actual;
    AnimarEscala escala;

    void Awake()
    {
        escala = texto.GetComponent<AnimarEscala>();
    }

    void OnEnable()
    {
        rana.FrutaJuntada += Sumar;
    }

    void OnDisable()
    {
        rana.FrutaJuntada -= Sumar;
    }

    void Sumar(Vector3 lugar, int ganados)
    {
        total = total + ganados;
        if (actual != null)
        {
            StopCoroutine(actual);
        }
        actual = StartCoroutine(Subir());
        texto.transform.localScale = Vector3.one * golpe;
        escala.Ir(1f, duracion, curvas.rebote);
    }

    IEnumerator Subir()
    {
        int inicio = mostrado;
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            mostrado = Mathf.RoundToInt(Mathf.Lerp(inicio, total, tiempo / duracion));
            texto.text = mostrado.ToString("00000");
            yield return null;
        }
    }
}
