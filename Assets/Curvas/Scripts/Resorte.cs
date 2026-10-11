using UnityEngine;

public class Resorte : MonoBehaviour
{
    [Header("Lanzar")]
    [Tooltip("La velocidad con la que lanza, hacia donde apunta el resorte")]
    [Range(5f, 30f)]
    [SerializeField] float fuerza = 15f;

    [Header("Dibujo")]
    [Tooltip("Recogido, esperando")]
    [SerializeField] Sprite recogido;
    [Tooltip("Estirado, justo después de lanzar")]
    [SerializeField] Sprite estirado;
    [Tooltip("Segundos que se queda estirado")]
    [SerializeField] float tiempoEstirado = 0.3f;

    SpriteRenderer dibujo;
    float reloj;

    void Start()
    {
        dibujo = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (reloj > 0f)
        {
            reloj -= Time.deltaTime;
            if (reloj <= 0f)
            {
                dibujo.sprite = recogido;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        Personaje personaje = otro.GetComponent<Personaje>();
        if (personaje != null)
        {
            personaje.Lanzar(transform.up * fuerza);
            dibujo.sprite = estirado;
            reloj = tiempoEstirado;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 inicio = transform.position;
        Vector2 velocidad = transform.up * fuerza;
        Vector2 anterior = inicio;

        for (int i = 1; i <= 30; i++)
        {
            float t = i * 0.1f;
            Vector2 punto = inicio + velocidad * t + Physics2D.gravity * (t * t / 2f);
            Gizmos.DrawLine(anterior, punto);
            anterior = punto;
        }
    }
}
