using UnityEngine;

public class Heroe : MonoBehaviour
{
    [SerializeField] string nombre = "Caballero";
    [SerializeField] float velocidad = 4.5f;

    void Start()
    {
        Debug.Log(nombre + " entra en la mazmorra. Velocidad: " + velocidad);
    }
}
