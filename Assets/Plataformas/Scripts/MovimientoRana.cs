using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoRana : MonoBehaviour
{
    [SerializeField] float velocidad = 5f;

    InputAction mover;
    Rigidbody2D cuerpo;
    float direccion;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mover = InputSystem.actions.FindAction("Move");
        cuerpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direccion = mover.ReadValue<Vector2>().x;
    }

    void FixedUpdate()
    {
        cuerpo.linearVelocity = new Vector2(direccion * velocidad, cuerpo.linearVelocity.y);
    }
}
