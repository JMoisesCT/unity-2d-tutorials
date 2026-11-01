using System.Collections;
using UnityEngine;

public class Director : MonoBehaviour
{
    [Header("Oleada")]
    [Tooltip("El asset con los pasos")]
    [SerializeField] Oleada oleada;
    [Tooltip("La x donde aparecen: fuera de la pantalla, a la derecha")]
    [SerializeField] float entrada = 10f;

    [Header("Carpetas")]
    [Tooltip("Dónde se guardan los enemigos en la Hierarchy")]
    [SerializeField] Transform enemigos;
    [Tooltip("Dónde guardan sus balas las torretas")]
    [SerializeField] Transform disparos;

    void Start()
    {
        StartCoroutine(Recorrer());
    }

    IEnumerator Recorrer()
    {
        foreach (Paso paso in oleada.pasos)
        {
            yield return new WaitForSeconds(paso.espera);
            Vector2 lugar = new Vector2(entrada, paso.altura);
            GameObject nuevo = Instantiate(paso.enemigo, lugar, paso.enemigo.transform.rotation, enemigos);
            if (nuevo.TryGetComponent(out Enemigo nave))
            {
                nave.Recorrer(paso.recorrido);
            }
            if (nuevo.TryGetComponent(out Torreta torreta))
            {
                torreta.UsarCarpeta(disparos);
            }
        }
        Debug.Log("Oleada: ya salieron todos");
        while (enemigos.childCount > 0)
        {
            yield return null;
        }
        Debug.Log("¡Oleada superada!");
    }
}