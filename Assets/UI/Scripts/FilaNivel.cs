using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilaNivel : MonoBehaviour
{
    [Header("Textos")]
    [Tooltip("El número del nivel.")]
    [SerializeField] TMP_Text numero;
    [Tooltip("El nombre del nivel.")]
    [SerializeField] TMP_Text nombre;
    [Header("Estrellas")]
    [Tooltip("Las imágenes de las estrellas, en orden.")]
    [SerializeField] Image[] estrellas;
    [Tooltip("El sprite de una estrella ganada.")]
    [SerializeField] Sprite llena;
    [Tooltip("El sprite de una estrella que falta.")]
    [SerializeField] Sprite vacia;

    public void Mostrar(int posicion, string titulo, int ganadas)
    {
        numero.text = posicion.ToString("00");
        nombre.text = titulo;
        for (int i = 0; i < estrellas.Length; i++)
        {
            if (i < ganadas)
            {
                estrellas[i].sprite = llena;
            }
            else
            {
                estrellas[i].sprite = vacia;
            }
        }
    }
}
