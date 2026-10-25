using UnityEngine;

public class Profundidad : MonoBehaviour
{
    [Tooltip("Cuántos números de orden caben en una unidad de altura")]
    [SerializeField] int precision = 100;
    [Tooltip("Lo que pisa el suelo. Vacío: este mismo objeto")]
    [SerializeField] Transform pies;

    SpriteRenderer dibujo;

    void Start()
    {
        dibujo = GetComponent<SpriteRenderer>();
        if (pies == null)
        {
            pies = transform;
        }
    }

    void LateUpdate()
    {
        dibujo.sortingOrder = -Mathf.RoundToInt(pies.position.y * precision);
    }
}
