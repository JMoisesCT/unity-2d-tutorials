using System.Collections.Generic;
using UnityEngine;

public class ZonaPelea : MonoBehaviour
{
    [Header("Oleadas")]
    [Tooltip("El prefab del Punk que aparece")]
    [SerializeField] Punk punk;
    [Tooltip("Dónde aparecen: un punk por punto en cada oleada")]
    [SerializeField] Transform[] apariciones;
    [Tooltip("Cuántas oleadas trae la zona")]
    [SerializeField] int oleadas = 2;

    [Header("Escena")]
    [Tooltip("La cámara que se congela mientras dura la pelea")]
    [SerializeField] CamaraCalle camara;

    [Header("Final")]
    [Tooltip("La barra del HUD para lo que aparece aquí; vacío en las zonas normales")]
    [SerializeField] BarraVida barraJefe;
    [Tooltip("Marcada solo en la última zona: al vencerla, termina el juego")]
    [SerializeField] bool final;

    Transform peleadora;
    List<Punk> vivos = new List<Punk>();
    int oleada;
    bool activa;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (!activa && otro.TryGetComponent(out Peleadora chica))
        {
            activa = true;
            peleadora = chica.transform;
            camara.Congelar(true);
            SoltarOleada();
        }
    }

    void SoltarOleada()
    {
        oleada++;
        Debug.Log("Oleada " + oleada);
        foreach (Transform punto in apariciones)
        {
            Punk nuevo = Instantiate(punk, punto.position, Quaternion.identity, transform);
            nuevo.Perseguir(peleadora);
            if (barraJefe != null)
            {
                barraJefe.gameObject.SetActive(true);
                nuevo.GetComponent<Vida>().MostrarEn(barraJefe);
            }
            vivos.Add(nuevo);
        }
    }

    void Update()
    {
        if (!activa)
        {
            return;
        }
        for (int i = vivos.Count - 1; i >= 0; i--)
        {
            if (vivos[i] == null)
            {
                vivos.RemoveAt(i);
            }
        }
        if (vivos.Count == 0)
        {
            if (oleada < oleadas)
            {
                SoltarOleada();
            }
            else
            {
                Debug.Log("¡Zona superada!");
                if (final)
                {
                    Debug.Log("¡Calle limpia! Fin del juego");
                }
                camara.Congelar(false);
                Destroy(gameObject);
            }
        }
    }
}
