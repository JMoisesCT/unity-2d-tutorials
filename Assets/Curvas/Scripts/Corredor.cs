using UnityEngine;

public class Corredor : Personaje
{
    [Header("Rodar")]
    [Tooltip("Cuánta velocidad pierde por segundo rodando")]
    [SerializeField] float friccionRodando = 0.5f;
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

    bool cargando;

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
            velocidadSuelo = Mathf.MoveTowards(velocidadSuelo, 0f, friccionRodando * Time.deltaTime);

            if (velocidadSuelo == 0f)
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
                dibujo.sprite = dePie;
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
    }
}
