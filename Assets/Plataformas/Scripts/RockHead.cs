using UnityEngine;
using TMPro;

public class RockHead : Enemigo
{
    enum Estado { Esperar, Embestir, Aturdido, Muerto }

    [SerializeField] float velocidad = 8f;
    [SerializeField] float distancia = 20f;
    [SerializeField] float espera = 1f;
    [SerializeField] float tiempoAturdido = 3f;
    [SerializeField] LayerMask capaRana;
    [SerializeField] LayerMask capaSuelo;
    [SerializeField] TMP_Text textoJefe;
    [SerializeField] GameObject meta;

    Estado estado = Estado.Esperar;
    float reloj;
    int vidas = 3;

    protected override void Start()
    {
        base.Start();
        Girar();
        reloj = espera;
    }

    void FixedUpdate()
    {
        Vector2 adelante = new Vector2(direccion, 0f);
        switch (estado)
        {
            case Estado.Esperar:
                reloj -= Time.fixedDeltaTime;
                RaycastHit2D rana = Physics2D.Raycast(transform.position, adelante, distancia, capaRana);
                if (reloj <= 0f && rana.collider != null)
                {
                    estado = Estado.Embestir;
                    animador.Play("RockHeadIdle");
                }
                break;

            case Estado.Embestir:
                cuerpo.linearVelocity = adelante * velocidad;
                RaycastHit2D pared = Physics2D.Raycast(transform.position, adelante, 1.2f, capaSuelo);
                if (pared.collider != null)
                {
                    cuerpo.linearVelocity = Vector2.zero;
                    estado = Estado.Aturdido;
                    reloj = tiempoAturdido;
                    animador.Play("RockHeadChoque");
                }
                break;

            case Estado.Aturdido:
                reloj -= Time.fixedDeltaTime;
                if (reloj <= 0f)
                {
                    Despertar();
                }
                break;
        }
    }

    void Despertar()
    {
        Girar();
        estado = Estado.Esperar;
        reloj = espera;
        animador.Play("RockHeadBlink");
    }

    public override void Pisado()
    {
        if (estado != Estado.Aturdido)
        {
            return;
        }
        vidas--;
        textoJefe.text = "Jefe: " + vidas;
        if (vidas > 0)
        {
            Despertar();
            animador.SetTrigger("Golpe");
        }
        else
        {
            estado = Estado.Muerto;
            meta.SetActive(true);
            base.Pisado();
        }
    }
}
