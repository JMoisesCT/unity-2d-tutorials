using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

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

    [Header("Disparo")]
    [Tooltip("El prefab que sale al disparar")]
    [SerializeField] Laser laser;
    [Tooltip("De dónde sale: la punta de la nave")]
    [SerializeField] Transform canon;
    [Tooltip("Segundos entre un disparo y el siguiente")]
    [SerializeField] float cadencia = 0.2f;
    [Tooltip("El sonido de cada disparo")]
    [SerializeField] AudioClip sonidoLaser;
    [Tooltip("Dónde se guardan los láseres en la Hierarchy")]
    [SerializeField] Transform carpeta;

    InputAction mover;
    InputAction disparar;
    Rigidbody2D cuerpo;
    Vector2 direccion;
    Vector2 minimo;
    Vector2 maximo;
    float siguienteDisparo;
    AudioSource altavoz;
    int creados;
    ObjectPool<Laser> cargador;

    void Start()
    {
        Debug.Log(nombre + " lista para despegar. Velocidad: " + velocidad);
        mover = InputSystem.actions.FindAction("Move");
        disparar = InputSystem.actions.FindAction("Attack");
        cuerpo = GetComponent<Rigidbody2D>();
        altavoz = GetComponent<AudioSource>();
        minimo = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maximo = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Debug.Log("Pantalla: de " + minimo + " a " + maximo);
        cargador = new ObjectPool<Laser>(
            () =>
            {
                creados++;
                Debug.Log("Láseres creados: " + creados);
                return Instantiate(laser, carpeta);
            },
            l => l.gameObject.SetActive(true),
            l => l.gameObject.SetActive(false));
    }

    void Update()
    {
        direccion = mover.ReadValue<Vector2>();
        if (disparar.IsPressed() && Time.time >= siguienteDisparo)
        {
            Laser nuevo = cargador.Get();
            nuevo.transform.SetPositionAndRotation(canon.position, canon.rotation);
            nuevo.UsarCargador(cargador);
            siguienteDisparo = Time.time + cadencia;
            altavoz.PlayOneShot(sonidoLaser);
        }
    }

    void FixedUpdate()
    {
        Vector2 siguiente = cuerpo.position + direccion * velocidad * Time.deltaTime;
        siguiente.x = Mathf.Clamp(siguiente.x, minimo.x + margen.x, maximo.x - margen.x);
        siguiente.y = Mathf.Clamp(siguiente.y, minimo.y + margen.y, maximo.y - margen.y);
        cuerpo.MovePosition(siguiente);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.TryGetComponent(out Vida vida))
        {
            Debug.Log("¡Choque con " + otro.name + "!");
        }
    }
}
