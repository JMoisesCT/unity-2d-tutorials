using UnityEngine;

public class Moneda : MonoBehaviour
{
    [Tooltip("Cuántas monedas suma al recogerla")]
    [SerializeField] int valor = 1;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.TryGetComponent(out Goblin goblin))
        {
            goblin.Recoger(valor);
            Destroy(gameObject);
        }
    }
}
