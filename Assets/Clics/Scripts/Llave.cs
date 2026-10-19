using UnityEngine;

public class Llave : MonoBehaviour
{
    [SerializeField] float radio = 0.5f;
    [SerializeField] ColorLlave color;

    Candado[] candados;
    Arrastrable arrastrable;

    void Start()
    {
        candados = FindObjectsByType<Candado>(FindObjectsSortMode.None);
        arrastrable = GetComponent<Arrastrable>();
    }

    void FixedUpdate()
    {
        if (arrastrable.EstaAgarrado() == false && arrastrable.EstaVolviendo() == false)
        {
            Encajar();
        }
    }

    void Encajar()
    {
        foreach (Candado candado in candados)
        {
            if (candado.gameObject.activeSelf == false)
            {
                continue;
            }

            float distancia = Vector2.Distance(transform.position, candado.transform.position);
            if (distancia < radio && candado.Acepta(color))
            {
                candado.Abrir();
                gameObject.SetActive(false);
            }
            else if (distancia < radio)
            {
                arrastrable.Volver();
            }
        }
    }
}
