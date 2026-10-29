using System.Collections;
using UnityEngine;

public class Generador : MonoBehaviour
{
    [Header("Luchadores")]
    [Tooltip("Los prefabs que puede crear: elige uno al azar cada vez")]
    [SerializeField] IA[] luchadores;
    [Tooltip("Segundos entre uno y el siguiente")]
    [SerializeField] float intervalo = 3f;
    [Tooltip("Cuántos de este generador pueden estar vivos a la vez")]
    [SerializeField] int maximo = 3;

    void Start()
    {
        StartCoroutine(Generar());
    }

    IEnumerator Generar()
    {
        while (true)
        {
            if (transform.childCount < maximo)
            {
                IA elegido = luchadores[Random.Range(0, luchadores.Length)];
                Instantiate(elegido, transform.position, Quaternion.identity, transform);
            }
            yield return new WaitForSeconds(intervalo);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
