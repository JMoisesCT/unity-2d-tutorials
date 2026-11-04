using UnityEngine;
using UnityEngine.InputSystem;

public class Rana : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] float velocidad = 2.5f;

    [Header("Sonidos")]
    [Tooltip("Suena al recoger una fruta")]
    [SerializeField] AudioClip sonidoFruta;
    [Tooltip("Suena al apretar Espacio")]
    [SerializeField] AudioClip sonidoBoing;
    [Tooltip("0 = mudo, 1 = volumen completo")]
    [SerializeField] float volumenBoing = 0.5f;

    int frutas = 0;
    InputAction mover;
    SpriteRenderer dibujo;
    Animator animador;
    AudioSource altavoz;

    void Start()
    {
        mover = InputSystem.actions.FindAction("Move");
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();
        altavoz = GetComponent<AudioSource>();
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

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            altavoz.PlayOneShot(sonidoBoing, volumenBoing);
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Fruta"))
        {
            frutas = frutas + 1;
            Debug.Log("Frutas: " + frutas);
            altavoz.pitch = Random.Range(0.9f, 1.1f);
            altavoz.PlayOneShot(sonidoFruta);
            Destroy(otro.gameObject);
        }
    }
}
