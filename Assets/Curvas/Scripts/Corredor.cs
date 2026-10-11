using UnityEngine;

public class Corredor : Personaje
{
    [Header("Rodar")]
    [Tooltip("Cuánta velocidad pierde por segundo rodando")]
    [SerializeField] float friccionRodando = 0.5f;
    [Tooltip("Cuánta velocidad pierde por segundo con la flecha contraria")]
    [SerializeField] float frenoRodando = 6f;
    [Tooltip("Grados que gira el dibujo por cada unidad de velocidad")]
    [SerializeField] float giroRodando = 120f;
    [Tooltip("La velocidad con la que sale el spin dash")]
    [SerializeField] float velocidadSpinDash = 15f;

    [Header("Dibujo")]
    [Tooltip("El Sprite Renderer del hijo Dibujo")]
    [SerializeField] SpriteRenderer dibujo;
    [Tooltip("De pie, en el suelo")]
    [SerializeField] Sprite dePie;
    [Tooltip("En el aire")]
    [SerializeField] Sprite enElAire;
    [Tooltip("Rodando, hecho bola")]
    [SerializeField] Sprite hechoBola;
    [Tooltip("Caminando, primer paso")]
    [SerializeField] Sprite paso1;
    [Tooltip("Caminando, segundo paso")]
    [SerializeField] Sprite paso2;
    [Tooltip("Segundos que dura cada paso al ir despacio")]
    [SerializeField] float pasoLento = 0.25f;
    [Tooltip("Segundos que dura cada paso a la velocidad máxima")]
    [SerializeField] float pasoRapido = 0.06f;

    [Header("Efectos")]
    [Tooltip("La estela que deja al ir rápido")]
    [SerializeField] TrailRenderer estela;
    [Tooltip("Desde esta velocidad se ve la estela")]
    [SerializeField] float velocidadEstela = 6f;
    [Tooltip("El polvo de los pies")]
    [SerializeField] ParticleSystem polvo;
    [Tooltip("Desde esta velocidad, frenar levanta polvo")]
    [SerializeField] float velocidadDerrape = 3f;

    bool cargando;
    float reloj;
    bool primerPaso;
    Estado estadoAnterior;

    protected override void FlechaAbajo()
    {
        estado = Estado.Rodando;
        cargando = Mathf.Abs(velocidadSuelo) < 0.5f;
    }

    protected override void Rodar()
    {
        if (cargando)
        {
            velocidadSuelo = 0f;

            if (mover.ReadValue<Vector2>().y >= 0f)
            {
                cargando = false;
                velocidadSuelo = velocidadSpinDash;
                if (dibujo.flipX)
                {
                    velocidadSuelo = -velocidadSpinDash;
                }
            }
        }
        else
        {
            float direccion = mover.ReadValue<Vector2>().x;
            float freno = friccionRodando;
            if (direccion * velocidadSuelo < 0f)
            {
                freno = frenoRodando;
            }
            velocidadSuelo = Mathf.MoveTowards(velocidadSuelo, 0f, freno * Time.deltaTime);

            if (Mathf.Abs(velocidadSuelo) < 0.5f)
            {
                estado = Estado.Suelo;
            }
        }
    }

    void LateUpdate()
    {
        float direccion = mover.ReadValue<Vector2>().x;
        if (direccion > 0f)
        {
            dibujo.flipX = false;
        }
        if (direccion < 0f)
        {
            dibujo.flipX = true;
        }

        switch (estado)
        {
            case Estado.Suelo:
                Caminar();
                dibujo.transform.localRotation = Quaternion.identity;
                break;

            case Estado.Aire:
                dibujo.sprite = enElAire;
                dibujo.transform.localRotation = Quaternion.identity;
                break;

            case Estado.Rodando:
                dibujo.sprite = hechoBola;
                float giro = velocidadSuelo;
                if (cargando)
                {
                    giro = velocidadSpinDash;
                    if (dibujo.flipX)
                    {
                        giro = -velocidadSpinDash;
                    }
                }
                dibujo.transform.Rotate(0f, 0f, -giro * giroRodando * Time.deltaTime);
                break;
        }

        Efectos();
    }

    void Caminar()
    {
        if (Mathf.Abs(velocidadSuelo) < 0.1f)
        {
            dibujo.sprite = dePie;
        }
        else
        {
            float rapidez = Mathf.Abs(velocidadSuelo) / velocidadMaxima;
            float duracionPaso = Mathf.Lerp(pasoLento, pasoRapido, rapidez);
            reloj += Time.deltaTime;
            if (reloj >= duracionPaso)
            {
                reloj = 0f;
                primerPaso = !primerPaso;
            }

            dibujo.sprite = paso2;
            if (primerPaso)
            {
                dibujo.sprite = paso1;
            }
        }
    }

    void Efectos()
    {
        estela.emitting = estado == Estado.Rodando || Mathf.Abs(velocidadSuelo) > velocidadEstela;

        float direccion = mover.ReadValue<Vector2>().x;
        bool frenando = direccion * velocidadSuelo < 0f;
        bool rapido = Mathf.Abs(velocidadSuelo) > velocidadDerrape;
        bool derrapando = estado == Estado.Suelo && frenando && rapido;
        if (derrapando && !polvo.isPlaying)
        {
            polvo.Play();
        }
        if (!derrapando && polvo.isPlaying)
        {
            polvo.Stop();
        }

        if (estadoAnterior == Estado.Aire && estado != Estado.Aire)
        {
            polvo.Emit(20);
        }
        estadoAnterior = estado;
    }
}
