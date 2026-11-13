using UnityEngine;
using UnityEngine.InputSystem;

public class RanaEfectos : MonoBehaviour
{
    [SerializeField] float velocidad = 2.5f;
    [Tooltip("Prefab de partículas que aparece al juntar una fruta")]
    [SerializeField] ParticleSystem estallido;

    int frutas = 0;
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

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Fruta"))
        {
            frutas = frutas + 1;
            Debug.Log("Frutas: " + frutas);
            ParticleSystem nuevo = Instantiate(estallido, otro.transform.position, Quaternion.identity);
            Fruta fruta = otro.GetComponent<Fruta>();
            var principal = nuevo.main;
            principal.startColor = fruta.ColorEstallido();
            Destroy(otro.gameObject);
        }
    }
}
