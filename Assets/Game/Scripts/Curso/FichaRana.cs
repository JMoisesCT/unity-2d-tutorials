using UnityEngine;

public class FichaRana : MonoBehaviour
{
    [SerializeField] string nombre = "Rana Ninja";
    [SerializeField] int vidas = 3;
    [SerializeField] float velocidad = 2.5f;
    [SerializeField] bool estaViva = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Me llamo " + nombre);
        Debug.Log("Vidas: " + vidas);
        Debug.Log("Velocidad: " + velocidad);
        Debug.Log("¿Está viva? " + estaViva);
        vidas = vidas - 1;
        Debug.Log("¡Auch! Le quedan " + vidas + " vidas.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
