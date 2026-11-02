using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Jefe : MonoBehaviour
{
    enum Fase { Abanicos, Circulos, Espiral }

    [Header("Balas")]
    [Tooltip("El prefab de la bala roja")]
    [SerializeField] Bala bala;
    [Tooltip("Dónde se guardan las balas en la Hierarchy")]
    [SerializeField] Transform carpeta;
    [Tooltip("A qué distancia del centro nacen")]
    [SerializeField] float radio = 1.4f;

    [Header("Patrones")]
    [Tooltip("Cuántas balas tiene el abanico")]
    [SerializeField] int balasAbanico = 7;
    [Tooltip("Grados entre la primera y la última bala del abanico")]
    [SerializeField] float apertura = 80f;
    [Tooltip("Cuántas balas tiene el círculo")]
    [SerializeField] int balasCirculo = 20;
    [Tooltip("Grados que gira la espiral entre bala y bala")]
    [SerializeField] float giroEspiral = 25f;

    [Header("Fases")]
    [Tooltip("Con menos de esta parte de la vida pasa a los círculos")]
    [SerializeField] float cambioCirculos = 0.66f;
    [Tooltip("Con menos de esta parte de la vida pasa a la espiral")]
    [SerializeField] float cambioEspiral = 0.33f;
    [Tooltip("Cuánto sube y baja desde el centro (fases 2 y 3)")]
    [SerializeField] float amplitud = 2.5f;
    [Tooltip("Qué tan rápido sube y baja")]
    [SerializeField] float frecuencia = 1.5f;
    [Tooltip("El sonido al cambiar de fase")]
    [SerializeField] AudioClip sonidoFase;

    [Header("Entrada")]
    [Tooltip("Hasta qué x avanza antes de disparar")]
    [SerializeField] float parada = 5.5f;
    [Tooltip("Unidades por segundo al entrar")]
    [SerializeField] float velocidadEntrada = 2f;

    ObjectPool<Bala> cargador;
    Vida vida;
    Fase fase;
    AudioSource altavoz;
    float vaiven;

    public void UsarCarpeta(Transform deDonde)
    {
        carpeta = deDonde;
    }

    void Start()
    {
        vida = GetComponent<Vida>();
        altavoz = GetComponent<AudioSource>();
        cargador = new ObjectPool<Bala>(
            () => Instantiate(bala, carpeta),
            b => b.gameObject.SetActive(true),
            b => b.gameObject.SetActive(false));
        StartCoroutine(Atacar());
    }

    void Update()
    {
        Fase nueva = ElegirFase();
        if (nueva != fase)
        {
            fase = nueva;
            altavoz.PlayOneShot(sonidoFase);
            Debug.Log("El jefe pasa a la fase " + fase);
        }
        if (fase != Fase.Abanicos)
        {
            vaiven += Time.deltaTime;
            float y = amplitud * Mathf.Sin(vaiven * frecuencia);
            transform.position = new Vector2(transform.position.x, y);
        }
    }

    Fase ElegirFase()
    {
        float queda = vida.Porcentaje();
        if (queda < cambioEspiral)
        {
            return Fase.Espiral;
        }
        if (queda < cambioCirculos)
        {
            return Fase.Circulos;
        }
        return Fase.Abanicos;
    }

    IEnumerator Atacar()
    {
        while (transform.position.x > parada)
        {
            transform.position += Vector3.left * velocidadEntrada * Time.deltaTime;
            yield return null;
        }
        float angulo = 0;
        while (true)
        {
            switch (fase)
            {
                case Fase.Abanicos:
                    Abanico();
                    yield return new WaitForSeconds(0.6f);
                    break;
                case Fase.Circulos:
                    Abanico();
                    yield return new WaitForSeconds(0.4f);
                    Circulo();
                    yield return new WaitForSeconds(0.8f);
                    break;
                case Fase.Espiral:
                    Disparar(angulo);
                    Disparar(angulo + 180);
                    angulo += giroEspiral;
                    yield return new WaitForSeconds(0.08f);
                    break;
            }
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
