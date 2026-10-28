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
    [Tooltip("Qué tan lejos y hacia dónde mira: un asset de PerfilIA")]
    [SerializeField] PerfilIA perfil;
    [Tooltip("Se voltea con el luchador: ahí aparece el arma")]
    [SerializeField] Transform mano;

    [Header("Paseo")]
    [Tooltip("Qué tan lejos de su lugar pasea")]
    [SerializeField] float radioPaseo = 2f;
    [Tooltip("Segundos entre un paseo y el siguiente")]
    [SerializeField] float pausa = 3f;

    [Header("Pelea")]
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
    float recuerdaHasta;

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
        Combatiente visto = ElegirRival();
        if (visto != null)
        {
            rival = visto;
            recuerdaHasta = Time.time + perfil.memoria;
        }
        else if (Time.time > recuerdaHasta)
        {
            rival = null;
        }

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
        if (rival != null)
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

    Combatiente ElegirRival()
    {
        Combatiente mejor = null;
        float mayor = 0;

        foreach (Combatiente otro in combatiente.Rivales())
        {
            if (Ve(otro.transform.position))
            {
                float puntaje = Puntaje(otro);
                if (puntaje > mayor)
                {
                    mayor = puntaje;
                    mejor = otro;
                }
            }
        }

        return mejor;
    }

    float Puntaje(Combatiente otro)
    {
        float interes = perfil.odio;
        if (otro.TryGetComponent(out Goblin goblin))
        {
            interes = goblin.Monedas() * perfil.codicia;
        }

        float distancia = Vector2.Distance(cuerpo.position, otro.transform.position);
        return interes - distancia;
    }

    bool Ve(Vector2 punto)
    {
        Vector2 hacia = punto - cuerpo.position;
        if (hacia.magnitude > perfil.radio)
        {
            return false;
        }

        if (Vector2.Angle(Mirada(), hacia) > perfil.cono / 2)
        {
            return false;
        }

        if (Physics2D.Linecast(cuerpo.position, punto, perfil.muros))
        {
            return false;
        }

        return true;
    }

    Vector2 Mirada()
    {
        if (dibujo.flipX)
        {
            return Vector2.left;
        }
        return Vector2.right;
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

    void OnDrawGizmosSelected()
    {
        Vector2 centro = transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centro, perfil.radio);

        if (dibujo == null)
        {
            dibujo = GetComponent<SpriteRenderer>();
        }

        Gizmos.color = Color.cyan;
        Vector2 borde1 = Quaternion.Euler(0, 0, perfil.cono / 2) * Mirada();
        Vector2 borde2 = Quaternion.Euler(0, 0, -perfil.cono / 2) * Mirada();
        Gizmos.DrawLine(centro, centro + borde1 * perfil.radio);
        Gizmos.DrawLine(centro, centro + borde2 * perfil.radio);

        if (rival != null)
        {
            Gizmos.color = Color.red;
            if (Ve(rival.transform.position))
            {
                Gizmos.color = Color.green;
            }
            Gizmos.DrawLine(centro, rival.transform.position);
        }
    }
}
