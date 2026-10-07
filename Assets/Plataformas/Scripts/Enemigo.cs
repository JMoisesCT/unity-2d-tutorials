using System.Collections;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] GameObject nube;

    protected Rigidbody2D cuerpo;
    protected SpriteRenderer dibujo;
    protected Animator animador;
    protected float direccion = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    protected void Girar()
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
