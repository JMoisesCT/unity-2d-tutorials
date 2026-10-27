using UnityEngine;

public class CamaraCalle : MonoBehaviour
{
    [Header("Seguir")]
    [Tooltip("A quién sigue la cámara: la Peleadora")]
    [SerializeField] Transform objetivo;
    [Tooltip("Unidades por segundo con las que alcanza a la chica; más que la velocidad de ella")]
    [SerializeField] float velocidad = 8f;

    [Header("Límites")]
    [Tooltip("La x más grande de la cámara: el final de la calle menos media pantalla")]
    [SerializeField] float final = 51f;
    [Tooltip("Hasta dónde puede ir la chica: unidades a cada lado del centro de la cámara")]
    [SerializeField] float distanciaBorde = 11f;

    bool congelada;

    void LateUpdate()
    {
        if (congelada)
        {
            return;
        }
        float x = Mathf.Max(transform.position.x, objetivo.position.x);
        x = Mathf.Min(x, final);
        x = Mathf.MoveTowards(transform.position.x, x, velocidad * Time.deltaTime);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public float BordeIzquierdo()
    {
        return transform.position.x - distanciaBorde;
    }

    public float BordeDerecho()
    {
        return transform.position.x + distanciaBorde;
    }

    public void Congelar(bool quieta)
    {
        congelada = quieta;
    }
}
