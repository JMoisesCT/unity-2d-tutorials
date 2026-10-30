using UnityEngine;

public class Nave : MonoBehaviour
{
    [Header("Ficha")]
    [Tooltip("Cómo se llama esta nave")]
    [SerializeField] string nombre = "Nave azul";
    [Tooltip("Unidades por segundo (la usaremos en la sesión 2)")]
    [SerializeField] float velocidad = 6f;

    void Start()
    {
        Debug.Log(nombre + " lista para despegar. Velocidad: " + velocidad);
    }
}
