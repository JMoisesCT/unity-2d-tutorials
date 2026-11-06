using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class DatosNivel
{
    public string nombre;
    [Range(0, 3)] public int estrellas;
}

public class ListaNiveles : MonoBehaviour
{
    [Header("Piezas")]
    [Tooltip("El prefab de una fila.")]
    [SerializeField] FilaNivel fila;
    [Tooltip("Dónde se crean las filas: el Content del Scroll View.")]
    [SerializeField] Transform contenido;
    [Header("Datos")]
    [Tooltip("Los niveles, en orden.")]
    [SerializeField] DatosNivel[] niveles;
    [Header("Al elegir")]
    [Tooltip("El texto de la etiqueta.")]
    [SerializeField] TMP_Text titulo;
    [Tooltip("El botón que empieza el nivel.")]
    [SerializeField] Button jugar;
    [Tooltip("La etiqueta que rebota al elegir.")]
    [SerializeField] Aparecer etiqueta;
    [Header("Aparición")]
    [Tooltip("Segundos entre una fila y la siguiente.")]
    [SerializeField] float pausa = 0.08f;
    [Tooltip("La ventana de esta lista.")]
    [SerializeField] Ventana ventana;

    int elegido;
    List<Aparecer> filas = new List<Aparecer>();

    void OnEnable()
    {
        ventana.AlAbrir += Entrar;
    }

    void OnDisable()
    {
        ventana.AlAbrir -= Entrar;
    }

    void Start()
    {
        jugar.onClick.AddListener(Jugar);
        int numero = 1;
        bool abierto = true;
        foreach (DatosNivel nivel in niveles)
        {
            FilaNivel nueva = Instantiate(fila, contenido);
            nueva.Mostrar(numero, nivel.nombre, nivel.estrellas);
            Button boton = nueva.GetComponent<Button>();
            boton.interactable = abierto;
            int este = numero;
            boton.onClick.AddListener(() => Elegir(este));
            filas.Add(nueva.GetComponent<Aparecer>());
            abierto = nivel.estrellas > 0;
            numero = numero + 1;
        }
    }

    void Entrar()
    {
        EventSystem.current.SetSelectedGameObject(filas[0].gameObject);
        float espera = pausa;
        foreach (Aparecer fila in filas)
        {
            fila.Mostrar(espera);
            espera = espera + pausa;
        }
    }

    void Elegir(int cual)
    {
        elegido = cual;
        titulo.text = niveles[cual - 1].nombre;
        jugar.interactable = true;
        etiqueta.Mostrar(0f);
        EventSystem.current.SetSelectedGameObject(jugar.gameObject);
    }

    void Jugar()
    {
        Debug.Log("A jugar el nivel " + elegido);
        Fundido.actual.Ir("Juego");
    }
}
