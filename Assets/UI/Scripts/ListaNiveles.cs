using UnityEngine;

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

    void Start()
    {
        int numero = 1;
        foreach (DatosNivel nivel in niveles)
        {
            FilaNivel nueva = Instantiate(fila, contenido);
            nueva.Mostrar(numero, nivel.nombre, nivel.estrellas);
            numero = numero + 1;
        }
    }
}
