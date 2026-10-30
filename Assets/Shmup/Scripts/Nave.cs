using UnityEngine;
using UnityEngine.InputSystem;

public class Nave : MonoBehaviour
{
    [Header("Ficha")]
    [Tooltip("Cómo se llama esta nave")]
    [SerializeField] string nombre = "Nave azul";
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 6f;

    [Header("Bordes")]
    [Tooltip("Media nave: cuánto se queda adentro el centro, en X y en Y")]
    [SerializeField] Vector2 margen = new Vector2(0.4f, 0.5f);

    InputAction mover;
    Rigidbody2D cuerpo;
    Vector2 direccion;
    Vector2 minimo;
    Vector2 maximo;

    void Start()
    {
        Debug.Log(nombre + " lista para despegar. Velocidad: " + velocidad);
        mover = InputSystem.actions.FindAction("Move");
        cuerpo = GetComponent<Rigidbody2D>();
        minimo = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maximo = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Debug.Log("Pantalla: de " + minimo + " a " + maximo);
    }

    void Update()
    {
        direccion = mover.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector2 siguiente = cuerpo.position + direccion * velocidad * Time.deltaTime;
        siguiente.x = Mathf.Clamp(siguiente.x, minimo.x + margen.x, maximo.x - margen.x);
        siguiente.y = Mathf.Clamp(siguiente.y, minimo.y + margen.y, maximo.y - margen.y);
        cuerpo.MovePosition(siguiente);
    }
}
