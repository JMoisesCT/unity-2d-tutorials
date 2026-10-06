using System.Collections;
using UnityEngine;

public class PlataformaCae : MonoBehaviour
{
    [SerializeField] float espera = 0.5f;

    Rigidbody2D cuerpo;
    Vector3 inicio;
    bool cayendo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        inicio = transform.position;
    }

    void OnCollisionEnter2D(Collision2D choque)
    {
        if (choque.gameObject.CompareTag("Player") && cayendo == false)
        {
            StartCoroutine(Caer());
        }
    }

    IEnumerator Caer()
    {
        cayendo = true;
        yield return new WaitForSeconds(espera);
        cuerpo.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(2f);
        cuerpo.bodyType = RigidbodyType2D.Kinematic;
        cuerpo.linearVelocity = Vector2.zero;
        transform.position = inicio;
        cayendo = false;
    }
}
