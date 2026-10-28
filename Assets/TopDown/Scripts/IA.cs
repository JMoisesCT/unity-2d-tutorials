using UnityEngine;

public class IA : MonoBehaviour
{
    enum Estado
    {
        Vagar,
        Perseguir,
        Atacar
    }

    [Header("Ficha")]
    [Tooltip("Los números del luchador: un asset de DatosHeroe")]
    [SerializeField] DatosHeroe datos;
    [Tooltip("Se voltea con el luchador: ahí aparece el arma")]
    [SerializeField] Transform mano;

    [Header("Paseo")]
    [Tooltip("Qué tan lejos de su lugar pasea")]
    [SerializeField] float radioPaseo = 2f;
    [Tooltip("Segundos entre un paseo y el siguiente")]
    [SerializeField] float pausa = 3f;

    [Header("Pelea")]
    [Tooltip("Distancia a la que ve a un rival")]
    [SerializeField] float vision = 6f;
    [Tooltip("A cuántas unidades del rival se para a golpear")]
    [SerializeField] float distanciaAtaque = 1f;

    Estado estado = Estado.Vagar;
    Rigidbody2D cuerpo;
    SpriteRenderer dibujo;
    Animator animador;
    Vector2 inicio;
    Vector2 paseo;
    float siguientePaseo;
    Combatiente combatiente;
    Combatiente rival;
    Ataque arma;
    float siguienteGolpe;

    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        animador.runtimeAnimatorController = datos.animaciones;
        inicio = cuerpo.position;
        paseo = inicio;
        combatiente = GetComponent<Combatiente>();
        GetComponent<Vida>().Empezar(datos.vida);
        arma = Instantiate(datos.arma, mano);
    }

    void FixedUpdate()
    {
        rival = combatiente.RivalMasCercano();
        ElegirEstado();

        switch (estado)
        {
            case Estado.Vagar:
                Vagar();
                break;
            case Estado.Perseguir:
                Caminar(PuestoDeAtaque(), datos.velocidad);
                break;
            case Estado.Atacar:
                Atacar();
                break;
        }
    }

    void ElegirEstado()
    {
        Estado nuevo = Estado.Vagar;
        if (rival != null && Vector2.Distance(cuerpo.position, rival.transform.position) < vision)
        {
            nuevo = Estado.Perseguir;
            if (PuedeAtacar())
            {
                nuevo = Estado.Atacar;
            }
        }

        if (nuevo != estado)
        {
            Debug.Log(name + ": " + estado + " → " + nuevo);
            estado = nuevo;
        }
    }

    void Vagar()
    {
        if (Time.time >= siguientePaseo)
        {
            paseo = inicio + Random.insideUnitCircle * radioPaseo;
            siguientePaseo = Time.time + pausa;
        }
        Caminar(paseo, datos.velocidad / 2);
    }

    void Atacar()
    {
        Quieto();
        Mirar(rival.transform.position.x - cuerpo.position.x);

        if (Time.time >= siguienteGolpe)
        {
            siguienteGolpe = Time.time + datos.cadencia;
            arma.Atacar(datos);
        }
    }

    bool PuedeAtacar()
    {
        Vector2 hacia = (Vector2)rival.transform.position - cuerpo.position;
        return Mathf.Abs(hacia.y) < 0.2f && Mathf.Abs(hacia.x) < distanciaAtaque + 0.2f;
    }

    Vector2 PuestoDeAtaque()
    {
        Vector2 suyo = rival.transform.position;
        if (cuerpo.position.x < suyo.x)
        {
            return suyo + Vector2.left * distanciaAtaque;
        }
        return suyo + Vector2.right * distanciaAtaque;
    }

    void Caminar(Vector2 destino, float rapidez)
    {
        Vector2 hacia = destino - cuerpo.position;
        if (hacia.magnitude < 0.1f)
        {
            Quieto();
            return;
        }

        cuerpo.linearVelocity = hacia.normalized * rapidez;
        animador.SetBool("Corriendo", true);
        Mirar(hacia.x);
    }

    void Quieto()
    {
        cuerpo.linearVelocity = Vector2.zero;
        animador.SetBool("Corriendo", false);
    }

    void Mirar(float haciaX)
    {
        if (haciaX < 0)
        {
            dibujo.flipX = true;
            mano.localScale = new Vector3(-1, 1, 1);
        }
        else if (haciaX > 0)
        {
            dibujo.flipX = false;
            mano.localScale = new Vector3(1, 1, 1);
        }
    }
}
