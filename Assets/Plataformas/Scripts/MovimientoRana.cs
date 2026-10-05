using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoRana : MonoBehaviour
{
    [SerializeField] float velocidad = 5f;
    [SerializeField] float fuerzaSalto = 8f;
    [SerializeField] Transform pies;
    [SerializeField] LayerMask capaSuelo;
    [SerializeField] int saltosMaximos = 2;

    InputAction mover;
    InputAction saltar;
    Rigidbody2D cuerpo;
    float direccion;
    bool quiereSaltar;
    bool soltoSalto;
    int saltosHechos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = InputSystem.actions.FindAction("Move");
        saltar = InputSystem.actions.FindAction("Jump");
        cuerpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direccion = mover.ReadValue<Vector2>().x;
        if (saltar.WasPressedThisFrame())
        {
            quiereSaltar = true;
        }
        if (saltar.WasReleasedThisFrame())
        {
            soltoSalto = true;
        }
    }

    void FixedUpdate()
    {
        cuerpo.linearVelocity = new Vector2(direccion * velocidad, cuerpo.linearVelocity.y);
        bool enSuelo = Physics2D.OverlapCircle(pies.position, 0.2f, capaSuelo);
        if (enSuelo && cuerpo.linearVelocity.y <= 0)
        {
            saltosHechos = 0;
        }
        if (quiereSaltar && saltosHechos < saltosMaximos)
        {
            cuerpo.linearVelocity = new Vector2(cuerpo.linearVelocity.x, fuerzaSalto);
            saltosHechos++;
        }
        quiereSaltar = false;
        if (soltoSalto && cuerpo.linearVelocity.y > 0)
        {
            cuerpo.linearVelocity = new Vector2(cuerpo.linearVelocity.x, cuerpo.linearVelocity.y * 0.5f);
        }
        soltoSalto = false;
    }
}
