using UnityEngine;

public class Llave : MonoBehaviour
{
    [SerializeField] float radio = 0.5f;
    [SerializeField] ColorLlave color;

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
            if (distancia < radio && candado.Acepta(color))
            {
                candado.Abrir();
                gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }
}
