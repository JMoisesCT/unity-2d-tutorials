using System.Collections;
using UnityEngine;

public class GolpeRana : MonoBehaviour
{
    [SerializeField] float fuerzaRetroceso = 6f;
    [SerializeField] float fuerzaRebote = 10f;
    [SerializeField] HUD hud;

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
        bool cae = cuerpo.linearVelocity.y < 0;
        bool encima = transform.position.y > otro.transform.position.y;
        if (otro.CompareTag("Enemigo") && cae && encima)
        {
            if (otro.TryGetComponent(out Enemigo enemigo))
            {
                enemigo.Pisado();
                cuerpo.linearVelocity = new Vector2(cuerpo.linearVelocity.x, fuerzaRebote);
            }
        }
        else if ((otro.CompareTag("Trampa") || otro.CompareTag("Enemigo")) && golpeada == false)
        {
            StartCoroutine(Golpe(otro.transform.position.x));
        }
    }

    IEnumerator Golpe(float xTrampa)
    {
        golpeada = true;
        GameManager.instancia.PerderVida();
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
        if (GameManager.instancia.vidas > 0)
        {
            transform.position = puntoControl;
            movimiento.enabled = true;
            golpeada = false;
        }
        else
        {
            hud.MostrarGameOver();
        }
    }

    public void GuardarPunto(Vector3 punto)
    {
        puntoControl = punto;
    }
}
