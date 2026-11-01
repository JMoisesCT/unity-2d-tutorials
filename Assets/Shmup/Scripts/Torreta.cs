using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Torreta : MonoBehaviour
{
    [Header("Balas")]
    [Tooltip("El prefab de la bala roja")]
    [SerializeField] Bala bala;
    [Tooltip("Dónde se guardan las balas en la Hierarchy")]
    [SerializeField] Transform carpeta;
    [Tooltip("A qué distancia del centro nacen")]
    [SerializeField] float radio = 0.6f;

    [Header("Patrones")]
    [Tooltip("Cuántas balas tiene el abanico")]
    [SerializeField] int balasAbanico = 5;
    [Tooltip("Grados entre la primera y la última bala del abanico")]
    [SerializeField] float apertura = 60f;
    [Tooltip("Cuántas balas tiene el círculo")]
    [SerializeField] int balasCirculo = 16;
    [Tooltip("Grados que gira la espiral entre bala y bala")]
    [SerializeField] float giroEspiral = 25f;

    ObjectPool<Bala> cargador;

    void Start()
    {
        cargador = new ObjectPool<Bala>(
            () => Instantiate(bala, carpeta),
            b => b.gameObject.SetActive(true),
            b => b.gameObject.SetActive(false));
        StartCoroutine(Atacar());
    }

    IEnumerator Atacar()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                Abanico();
                yield return new WaitForSeconds(0.6f);
            }
            for (int i = 0; i < 2; i++)
            {
                Circulo();
                yield return new WaitForSeconds(1f);
            }
            float angulo = 0;
            for (int i = 0; i < 40; i++)
            {
                Disparar(angulo);
                angulo += giroEspiral;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void Abanico()
    {
        float paso = apertura / (balasAbanico - 1);
        for (int i = 0; i < balasAbanico; i++)
        {
            Disparar(180 - apertura / 2 + paso * i);
        }
    }

    void Circulo()
    {
        for (int i = 0; i < balasCirculo; i++)
        {
            Disparar(360f / balasCirculo * i);
        }
    }

    void Disparar(float angulo)
    {
        float radianes = angulo * Mathf.Deg2Rad;
        Vector2 direccion = new Vector2(Mathf.Cos(radianes), Mathf.Sin(radianes));
        Bala nueva = cargador.Get();
        nueva.transform.position = (Vector2)transform.position + direccion * radio;
        nueva.transform.rotation = Quaternion.Euler(0, 0, angulo - 90);
        nueva.UsarCargador(cargador);
    }
}