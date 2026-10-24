using UnityEngine;

public class Perseguidor : MonoBehaviour
{
    [Header("Persecución")]
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 2f;
    [Tooltip("Distancia a la que ve al héroe")]
    [SerializeField] float vision = 2.5f;

    [Header("Ataque")]
    [Tooltip("Vida que quita al tocar")]
    [SerializeField] int daño = 1;

    Transform objetivo;
    Rigidbody2D cuerpo;
    SpriteRenderer dibujo;
    Animator animador;

    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        objetivo = FindAnyObjectByType<Heroe>().transform;
    }

    void FixedUpdate()
    {
        if (objetivo == null)
        {
            cuerpo.linearVelocity = Vector2.zero;
            animador.SetBool("Corriendo", false);
            return;
        }

        float distancia = Vector2.Distance(cuerpo.position, objetivo.position);
        bool loVe = distancia < vision;

        if (loVe)
        {
            Vector2 direccion = ((Vector2)objetivo.position - cuerpo.position).normalized;
            cuerpo.linearVelocity = direccion * velocidad;
            dibujo.flipX = direccion.x < 0;
        }
        else
        {
            cuerpo.linearVelocity = Vector2.zero;
        }

        animador.SetBool("Corriendo", loVe);
    }

    void OnCollisionStay2D(Collision2D choque)
    {
        if (choque.transform == objetivo && choque.gameObject.TryGetComponent(out Vida vida))
        {
            vida.RecibirDaño(daño);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, vision);
    }
}
