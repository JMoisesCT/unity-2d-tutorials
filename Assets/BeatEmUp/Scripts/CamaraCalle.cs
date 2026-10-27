using UnityEngine;

public class CamaraCalle : MonoBehaviour
{
    [Header("Seguir")]
    [Tooltip("A quién sigue la cámara: la Peleadora")]
    [SerializeField] Transform objetivo;

    [Header("Límites")]
    [Tooltip("La x más grande de la cámara: el final de la calle menos media pantalla")]
    [SerializeField] float final = 51f;
    [Tooltip("Hasta dónde puede volver la chica: unidades a la izquierda del centro de la cámara")]
    [SerializeField] float distanciaBorde = 11f;

    void LateUpdate()
    {
        float x = Mathf.Max(transform.position.x, objetivo.position.x);
        x = Mathf.Min(x, final);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public float BordeIzquierdo()
    {
        return transform.position.x - distanciaBorde;
    }
}
