using UnityEngine;

public class Pocion : MonoBehaviour
{
    [Tooltip("La ficha del héroe en que convierte")]
    [SerializeField] DatosHeroe ficha;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.TryGetComponent(out Heroe heroe))
        {
            heroe.Transformar(ficha);
            Destroy(gameObject);
        }
    }
}
