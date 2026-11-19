using System.Collections;
using UnityEngine;
using TMPro;

public class CajaDialogo : MonoBehaviour
{
    [Header("Piezas de la caja")]
    [SerializeField] GameObject caja;
    [SerializeField] TMP_Text nombre;
    [SerializeField] TMP_Text frase;

    [Header("Máquina de escribir")]
    [Tooltip("Segundos entre una letra y la siguiente")]
    [SerializeField] float pausa = 0.04f;

    [Header("Voz")]
    [Tooltip("Sonido que suena con cada letra")]
    [SerializeField] AudioClip voz;

    string[] frases;
    int indice;
    Coroutine escribiendo;
    AudioSource altavoz;

    void Start()
    {
        caja.SetActive(false);
        altavoz = GetComponent<AudioSource>();
    }

    public bool Abierta()
    {
        return caja.activeSelf;
    }

    public void Empezar(string quien, string[] lista)
    {
        frases = lista;
        indice = 0;
        nombre.text = quien;
        caja.SetActive(true);
        escribiendo = StartCoroutine(Escribir(frases[indice]));
    }

    public void Siguiente()
    {
        if (escribiendo != null)
        {
            StopCoroutine(escribiendo);
            escribiendo = null;
            frase.maxVisibleCharacters = frases[indice].Length;
            return;
        }

        indice = indice + 1;
        if (indice < frases.Length)
        {
            escribiendo = StartCoroutine(Escribir(frases[indice]));
        }
        else
        {
            caja.SetActive(false);
        }
    }

    IEnumerator Escribir(string texto)
    {
        frase.text = texto;
        frase.maxVisibleCharacters = 0;
        for (int i = 1; i <= texto.Length; i++)
        {
            yield return new WaitForSeconds(pausa);
            frase.maxVisibleCharacters = i;
            if (texto[i - 1] != ' ')
            {
                altavoz.pitch = Random.Range(0.9f, 1.1f);
                altavoz.PlayOneShot(voz);
            }
        }
        escribiendo = null;
    }
}
