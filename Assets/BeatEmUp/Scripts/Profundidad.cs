using UnityEngine;

public class Profundidad : MonoBehaviour
{
    [Tooltip("Cuántos números de orden caben en una unidad de altura")]
    [SerializeField] int precision = 100;

    SpriteRenderer dibujo;

    void Start()
    {
        dibujo = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        dibujo.sortingOrder = -Mathf.RoundToInt(transform.position.y * precision);
    }
}
