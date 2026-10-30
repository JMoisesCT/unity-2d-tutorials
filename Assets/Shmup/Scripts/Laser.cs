using UnityEngine;

public class Laser : MonoBehaviour
{
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 15f;

    void Update()
    {
        transform.Translate(Vector2.up * velocidad * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
