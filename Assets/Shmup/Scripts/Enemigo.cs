using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Tooltip("Unidades por segundo, hacia la izquierda")]
    [SerializeField] float velocidad = 4f;

    [Header("Seno")]
    [Tooltip("Cuánto sube y baja desde su altura")]
    [SerializeField] float amplitud = 1.5f;
    [Tooltip("Qué tan rápido sube y baja")]
    [SerializeField] float frecuencia = 3f;

    Rigidbody2D cuerpo;
    Recorrido recorrido;
    float altura;
    float inicio;

    public void Recorrer(Recorrido cual)
    {
        recorrido = cual;
    }

    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        altura = transform.position.y;
        inicio = Time.time;
    }

    void FixedUpdate()
    {
        Vector2 siguiente = cuerpo.position + Vector2.left * velocidad * Time.deltaTime;
        if (recorrido == Recorrido.Seno)
        {
            siguiente.y = altura + amplitud * Mathf.Sin((Time.time - inicio) * frecuencia);
        }
        cuerpo.MovePosition(siguiente);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}