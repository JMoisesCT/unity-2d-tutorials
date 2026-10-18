using UnityEngine;

public class Nivel : MonoBehaviour
{
    int candados;
    [SerializeField] Puerta puerta;

    void Start()
    {
        candados = FindObjectsByType<Candado>(FindObjectsSortMode.None).Length;
        Debug.Log("Abre los " + candados + " candados para salir.");
    }

    public void CandadoAbierto()
    {
        candados -= 1;
        Debug.Log("Candados cerrados: " + candados);
        if (candados == 0)
        {
            Debug.Log("¡La puerta se abrió!");
            puerta.Abrir();
        }
    }
}
