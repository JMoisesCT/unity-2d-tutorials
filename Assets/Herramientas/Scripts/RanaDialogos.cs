using UnityEngine;
using UnityEngine.InputSystem;

public class RanaDialogos : MonoBehaviour
{
    [SerializeField] float velocidad = 2.5f;
    [Tooltip("Mientras la caja está abierta, la rana no camina")]
    [SerializeField] CajaDialogo caja;

    InputAction mover;
    SpriteRenderer dibujo;
    Animator animador;

    void Start()
    {
        mover = InputSystem.actions.FindAction("Move");
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
    }

    void Update()
    {
        if (caja.Abierta())
        {
            animador.SetBool("Corriendo", false);
            return;
        }

        Vector2 direccion = mover.ReadValue<Vector2>();
        transform.Translate(direccion * velocidad * Time.deltaTime);

        if (direccion.x < 0)
        {
            dibujo.flipX = true;
        }
        else if (direccion.x > 0)
        {
            dibujo.flipX = false;
        }
        animador.SetBool("Corriendo", direccion != Vector2.zero);
    }
}
