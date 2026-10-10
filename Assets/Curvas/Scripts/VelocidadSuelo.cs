using UnityEngine;
using UnityEngine.InputSystem;

public class VelocidadSuelo : MonoBehaviour
{
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
    [SerializeField] float velocidadSuelo;

    Rigidbody2D cuerpo;
    SensorSuelo sensor;
    InputAction mover;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        sensor = GetComponent<SensorSuelo>();
        mover = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        if (sensor.enSuelo)
        {
            velocidadSuelo -= fuerzaPendiente * Mathf.Sin(sensor.angulo * Mathf.Deg2Rad) * Time.deltaTime;

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

            cuerpo.linearVelocity = sensor.tangente * velocidadSuelo;
        }
    }
}
