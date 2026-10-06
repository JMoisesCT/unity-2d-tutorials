using UnityEngine;

public class AnimacionRana : MonoBehaviour
{
    Animator animador;
    Rigidbody2D cuerpo;
    SpriteRenderer dibujo;
    MovimientoRana movimiento;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animador = GetComponent<Animator>();
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        movimiento = GetComponent<MovimientoRana>();
    }

    // Update is called once per frame
    void Update()
    {
        float velocidadX = cuerpo.linearVelocity.x;
        animador.SetFloat("Velocidad", Mathf.Abs(velocidadX));
        animador.SetFloat("VelocidadY", cuerpo.linearVelocity.y);
        animador.SetBool("EnSuelo", movimiento.EstaEnSuelo());

        if (velocidadX > 0.1f)
        {
            dibujo.flipX = false;
        }
        else if (velocidadX < -0.1f)
        {
            dibujo.flipX = true;
        }
    }

    public void DobleSalto()
    {
        animador.SetTrigger("DobleSalto");
    }
}
