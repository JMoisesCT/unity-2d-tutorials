using UnityEngine;

public class Llave : MonoBehaviour
{
    [SerializeField] float radio = 0.5f;

    Candado[] candados;

    void Start()
    {
        candados = FindObjectsByType<Candado>(FindObjectsSortMode.None);
    }

    public bool Encajar()
    {
        foreach (Candado candado in candados)
        {
            float distancia = Vector2.Distance(transform.position, candado.transform.position);
            if (distancia < radio)
            {
                transform.position = candado.transform.position;
                return true;
            }
        }
        return false;
    }
}
