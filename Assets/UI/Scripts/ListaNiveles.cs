using TMPro;
using UnityEngine;
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

    int elegido;

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
            abierto = nivel.estrellas > 0;
            numero = numero + 1;
        }
    }

    void Elegir(int cual)
    {
        elegido = cual;
        titulo.text = niveles[cual - 1].nombre;
        jugar.interactable = true;
    }

    void Jugar()
    {
        Debug.Log("A jugar el nivel " + elegido);
    }
}
