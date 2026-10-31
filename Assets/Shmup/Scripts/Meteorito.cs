using UnityEngine;

public class Meteorito : MonoBehaviour
{
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 3f;
    [Tooltip("Grados por segundo, como máximo, a cada lado")]
    [SerializeField] float giro = 90f;

    void Start()
    {
        Vector2 direccion = new Vector2(-1, Random.Range(-0.5f, 0.5f));
        Debug.Log(name + " va hacia " + direccion);
        Rigidbody2D cuerpo = GetComponent<Rigidbody2D>();
        cuerpo.linearVelocity = direccion * velocidad;
        cuerpo.angularVelocity = Random.Range(-giro, giro);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
