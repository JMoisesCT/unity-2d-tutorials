using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CajaDialogo : MonoBehaviour
{
    [Header("Piezas de la caja")]
    [SerializeField] GameObject caja;
    [SerializeField] TMP_Text nombre;
    [SerializeField] TMP_Text frase;
    [SerializeField] Image etiqueta;
    [SerializeField] Image retrato;

    [Header("Máquina de escribir")]
    [Tooltip("Segundos entre una letra y la siguiente")]
    [SerializeField] float pausa = 0.04f;

    [Header("Voz")]
    [Tooltip("Sonido que suena con cada letra")]
    [SerializeField] AudioClip voz;

    [Header("Respuestas")]
    [Tooltip("El panel donde aparecen los botones")]
    [SerializeField] Transform opciones;
    [Tooltip("El prefab de un botón de respuesta")]
    [SerializeField] Button botonOpcion;

    Dialogo dialogo;
    int indice;
    Coroutine escribiendo;
    AudioSource altavoz;
    bool eligiendo;
    Charla charla;

    void Start()
    {
        caja.SetActive(false);
        altavoz = GetComponent<AudioSource>();
    }

    public bool Abierta()
    {
        return caja.activeSelf;
    }

    public void Empezar(Dialogo nuevo, Charla quien)
    {
        charla = quien;
        dialogo = nuevo;
        indice = 0;
        caja.SetActive(true);
        Mostrar();
    }

    public void Siguiente()
    {
        if (escribiendo != null)
        {
            StopCoroutine(escribiendo);
            escribiendo = null;
            frase.maxVisibleCharacters = dialogo.lineas[indice].texto.Length;
            return;
        }
        if (eligiendo)
        {
            return;
        }

        indice = indice + 1;
        if (indice < dialogo.lineas.Length)
        {
            Mostrar();
        }
        else if (dialogo.opciones.Length > 0)
        {
            MostrarOpciones();
        }
        else
        {
            caja.SetActive(false);
            charla.Termino(dialogo);
        }
    }

    void MostrarOpciones()
    {
        eligiendo = true;
        foreach (Opcion opcion in dialogo.opciones)
        {
            Button boton = Instantiate(botonOpcion, opciones);
            boton.GetComponentInChildren<TMP_Text>().text = opcion.texto;
            boton.onClick.AddListener(() => Elegir(opcion));
        }
        EventSystem.current.SetSelectedGameObject(opciones.GetChild(0).gameObject);
    }

    void Elegir(Opcion opcion)
    {
        eligiendo = false;
        foreach (Transform hijo in opciones)
        {
            Destroy(hijo.gameObject);
        }
        Empezar(opcion.siguiente, charla);
    }

    void Mostrar()
    {
        Linea linea = dialogo.lineas[indice];
        nombre.text = linea.quien.nombre;
        etiqueta.color = linea.quien.color;
        retrato.sprite = linea.quien.retrato;
        escribiendo = StartCoroutine(Escribir(linea.texto, linea.quien.tono));
    }

    IEnumerator Escribir(string texto, float tono)
    {
        frase.text = texto;
        frase.maxVisibleCharacters = 0;
        for (int i = 1; i <= texto.Length; i++)
        {
            yield return new WaitForSeconds(pausa);
            frase.maxVisibleCharacters = i;
            if (texto[i - 1] != ' ')
            {
                altavoz.pitch = tono * Random.Range(0.9f, 1.1f);
                altavoz.PlayOneShot(voz);
            }
        }
        escribiendo = null;
    }
}
