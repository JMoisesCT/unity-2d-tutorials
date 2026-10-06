using UnityEngine;

public class Trampolin : MonoBehaviour
{
    [SerializeField] float fuerza = 22f;

    Animator animador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animador = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D choque)
    {
        Vector2 normal = choque.GetContact(0).normal;
        if (choque.gameObject.CompareTag("Player") && normal.y < -0.5f)
        {
            choque.rigidbody.linearVelocity = new Vector2(choque.rigidbody.linearVelocity.x, fuerza);
            animador.SetTrigger("Rebotar");
        }
    }
}
