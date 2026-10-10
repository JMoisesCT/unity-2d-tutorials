using UnityEngine;
using UnityEngine.InputSystem;

public class Personaje : MonoBehaviour
{
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

    [Header("Para mirar en Play")]
    [SerializeField] bool enSuelo;
    [SerializeField] float angulo;
    [SerializeField] float velocidadSuelo;

    Vector2 tangente;
    Rigidbody2D cuerpo;
    InputAction mover;

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

        if (enSuelo)
        {
            AplicarPendiente();

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

            cuerpo.linearVelocity = tangente * velocidadSuelo;
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
}
