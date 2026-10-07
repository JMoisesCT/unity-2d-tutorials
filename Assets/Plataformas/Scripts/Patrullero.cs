using System.Collections;
using UnityEngine;

public class Patrullero : MonoBehaviour
{
    [SerializeField] float velocidad = 2f;
    [SerializeField] LayerMask capaSuelo;
    [SerializeField] GameObject nube;

    Rigidbody2D cuerpo;
    SpriteRenderer dibujo;
    Animator animador;
    float direccion = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 frente = new Vector2(transform.position.x + direccion * 0.7f, transform.position.y);
        RaycastHit2D suelo = Physics2D.Raycast(frente, Vector2.down, 1.2f, capaSuelo);
        Debug.DrawRay(frente, Vector2.down * 1.2f, Color.yellow);

        Vector2 adelante = new Vector2(direccion, 0f);
        RaycastHit2D pared = Physics2D.Raycast(transform.position, adelante, 0.8f, capaSuelo);
        Debug.DrawRay(transform.position, adelante * 0.8f, Color.red);

        if (suelo.collider == null || pared.collider != null)
        {
            Girar();
        }
        cuerpo.linearVelocity = new Vector2(direccion * velocidad, 0f);
    }

    void Girar()
    {
        direccion = -direccion;
        dibujo.flipX = direccion < 0;
    }

    public void Pisado()
    {
        StartCoroutine(Morir());
    }

    IEnumerator Morir()
    {
        enabled = false;
        cuerpo.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        animador.SetTrigger("Golpe");
        yield return new WaitForSeconds(0.35f);
        GameObject copia = Instantiate(nube, transform.position, Quaternion.identity);
        Destroy(copia, 0.35f);
        Destroy(gameObject);
    }
}
