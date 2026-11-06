using System.Collections;
using UnityEngine;

public class Aparecer : MonoBehaviour
{
    [Tooltip("Cuántos segundos tarda en aparecer.")]
    [SerializeField] float duracion = 0.4f;
    [Tooltip("El asset con las curvas de toda la interfaz.")]
    [SerializeField] Curvas curvas;

    AnimarEscala escala;

    void Awake()
    {
        escala = GetComponent<AnimarEscala>();
    }

    public void Mostrar(float espera)
    {
        StartCoroutine(Esperar(espera));
    }

    IEnumerator Esperar(float espera)
    {
        transform.localScale = Vector3.zero;
        yield return new WaitForSecondsRealtime(espera);
        escala.Ir(1f, duracion, curvas.rebote);
    }
}
