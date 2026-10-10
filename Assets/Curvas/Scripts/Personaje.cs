using UnityEngine;
using UnityEngine.InputSystem;

public class Personaje : MonoBehaviour
{
    protected enum Estado { Suelo, Aire, Rodando }

    [Header("Leer el suelo")]
    [Tooltip("Hasta dónde miran los rayos, desde el centro")]
    [SerializeField] float largoRayo = 1.5f;
    [Tooltip("Cuánto se separa cada pie del centro")]
    [SerializeField] float separacionPies = 0.3f;
    [Tooltip("Las capas que cuentan como suelo")]
    [SerializeField] LayerMask capaSuelo;

    [Header("Correr")]
    [Tooltip("La velocidad más alta que alcanza con las flechas")]
    [SerializeField] float velocidadMaxima = 8f;
    [Tooltip("Cuánta velocidad gana por segundo con una flecha pulsada")]
    [SerializeField] float aceleracion = 8f;
    [Tooltip("Cuánta velocidad pierde por segundo sin flechas")]
    [SerializeField] float friccion = 6f;

    [Header("Pendiente")]
    [Tooltip("Cuánto empuja la cuesta: acelera en bajada y frena en subida")]
    [SerializeField] float fuerzaPendiente = 20f;

    [Header("Paredes y techos")]
    [Tooltip("Desde esta inclinación, en grados, cuenta como pared")]
    [SerializeField] float anguloPared = 80f;
    [Tooltip("Por debajo de esta velocidad, se cae de paredes y techos")]
    [SerializeField] float velocidadMinimaPared = 3f;

    [Header("Para mirar en Play")]
    [SerializeField] protected Estado estado;
    [SerializeField] bool enSuelo;
    [SerializeField] float angulo;
    [SerializeField] protected float velocidadSuelo;

    Vector2 tangente;
    Rigidbody2D cuerpo;
    protected InputAction mover;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        mover = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        LeerSuelo();

        switch (estado)
        {
            case Estado.Suelo:
                AplicarPendiente();
                Correr();
                if (mover.ReadValue<Vector2>().y < 0f)
                {
                    FlechaAbajo();
                }
                SeguirSuelo();
                break;

            case Estado.Aire:
                cuerpo.rotation = 0f;
                if (enSuelo)
                {
                    velocidadSuelo = Vector2.Dot(cuerpo.linearVelocity, tangente);
                    estado = Estado.Suelo;
                }
                break;

            case Estado.Rodando:
                AplicarPendiente();
                Rodar();
                SeguirSuelo();
                break;
        }
    }

    void Correr()
    {
        float direccion = mover.ReadValue<Vector2>().x;
        float objetivo = direccion * velocidadMaxima;

        if (direccion != 0)
        {
            velocidadSuelo = Mathf.MoveTowards(velocidadSuelo, objetivo, aceleracion * Time.deltaTime);
        }
        else
        {
            velocidadSuelo = Mathf.MoveTowards(velocidadSuelo, 0f, friccion * Time.deltaTime);
        }
    }

    void SeguirSuelo()
    {
        if (enSuelo && !SeCaeDeLaPared())
        {
            cuerpo.linearVelocity = tangente * velocidadSuelo;
        }
        else
        {
            estado = Estado.Aire;
        }
    }

    void LeerSuelo()
    {
        Vector2 centro = transform.position;
        Vector2 abajo = -transform.up;
        Vector2 lado = transform.right * separacionPies;

        RaycastHit2D pieIzq = Physics2D.Raycast(centro - lado, abajo, largoRayo, capaSuelo);
        RaycastHit2D pieDer = Physics2D.Raycast(centro + lado, abajo, largoRayo, capaSuelo);
        Debug.DrawRay(centro - lado, abajo * largoRayo, Color.white);
        Debug.DrawRay(centro + lado, abajo * largoRayo, Color.white);
        enSuelo = pieIzq.collider != null && pieDer.collider != null;

        if (enSuelo)
        {
            tangente = (pieDer.point - pieIzq.point).normalized;
            Debug.DrawRay(pieIzq.point, tangente, Color.cyan);

            angulo = Vector2.SignedAngle(Vector2.right, tangente);
            cuerpo.rotation = angulo;
        }
    }

    protected virtual void AplicarPendiente()
    {
        velocidadSuelo -= fuerzaPendiente * Mathf.Sin(angulo * Mathf.Deg2Rad) * Time.deltaTime;
    }

    protected virtual bool SeCaeDeLaPared()
    {
        return Mathf.Abs(angulo) > anguloPared && Mathf.Abs(velocidadSuelo) < velocidadMinimaPared;
    }

    protected virtual void FlechaAbajo()
    {
    }

    protected virtual void Rodar()
    {
    }
}
