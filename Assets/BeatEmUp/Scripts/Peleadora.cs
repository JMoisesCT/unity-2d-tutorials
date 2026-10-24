using UnityEngine;

public class Peleadora : MonoBehaviour
{
    [SerializeField] string nombre = "Brawler Girl";
    [SerializeField] float velocidad = 4.5f;

    void Start()
    {
        Debug.Log(nombre + " llega a la calle. Velocidad: " + velocidad);
    }
}
