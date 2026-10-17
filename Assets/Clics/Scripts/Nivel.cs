using UnityEngine;

public class Nivel : MonoBehaviour
{
    [SerializeField] int candados = 3;

    void Start()
    {
        Debug.Log("Abre los " + candados + " candados para salir.");
    }
}
