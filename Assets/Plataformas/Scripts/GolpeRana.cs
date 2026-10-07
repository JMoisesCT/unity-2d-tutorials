using System.Collections;
using UnityEngine;

public class GolpeRana : MonoBehaviour
{
    [SerializeField] float fuerzaRetroceso = 6f;

    Rigidbody2D cuerpo;
    Animator animador;
    MovimientoRana movimiento;
    Vector3 puntoControl;
    bool golpeada;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        animador = GetComponent<Animator>();
        movimiento = GetComponent<MovimientoRana>();
        puntoControl = transform.position;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if ((otro.CompareTag("Trampa") || otro.CompareTag("Enemigo")) && golpeada == false)
        {
            StartCoroutine(Golpe(otro.transform.position.x));
        }
    }

    IEnumerator Golpe(float xTrampa)
    {
        golpeada = true;
        movimiento.enabled = false;
        animador.SetTrigger("Golpe");
        float lado = 1f;
        if (transform.position.x < xTrampa)
        {
            lado = -1f;
        }
        cuerpo.linearVelocity = new Vector2(lado * fuerzaRetroceso, fuerzaRetroceso);
        yield return new WaitForSeconds(0.5f);
        cuerpo.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        transform.position = puntoControl;
        movimiento.enabled = true;
        golpeada = false;
    }

    public void GuardarPunto(Vector3 punto)
    {
        puntoControl = punto;
    }
}
