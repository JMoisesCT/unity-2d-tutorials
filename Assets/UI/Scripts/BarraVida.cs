using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    [Tooltip("La vida que muestra esta barra.")]
    [SerializeField] Vida vida;
    [Tooltip("La imagen Filled que baja de golpe.")]
    [SerializeField] Image relleno;
    [Header("Estela")]
    [Tooltip("La imagen de atrás, que baja después.")]
    [SerializeField] Image estela;
    [Tooltip("Cuántos segundos se queda quieta.")]
    [SerializeField] float espera = 0.4f;
    [Tooltip("Cuántos segundos tarda en alcanzar la vida.")]
    [SerializeField] float duracion = 0.5f;
    [Tooltip("El asset con las curvas de toda la interfaz.")]
    [SerializeField] Curvas curvas;

    Coroutine actual;

    void OnEnable()
    {
        vida.Cambio += Mostrar;
    }

    void OnDisable()
    {
        vida.Cambio -= Mostrar;
    }

    void Mostrar(int valor, int maxima)
    {
        float parte = (float)valor / maxima;
        relleno.fillAmount = parte;
        if (actual != null)
        {
            StopCoroutine(actual);
        }
        actual = StartCoroutine(Alcanzar(parte));
    }

    IEnumerator Alcanzar(float destino)
    {
        yield return new WaitForSeconds(espera);
        float inicio = estela.fillAmount;
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float parte = curvas.suave.Evaluate(tiempo / duracion);
            estela.fillAmount = Mathf.Lerp(inicio, destino, parte);
            yield return null;
        }
    }
}
