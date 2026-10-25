using UnityEngine;

public class Punk : MonoBehaviour
{
    [Header("Persecución")]
    [Tooltip("La peleadora a la que persigue")]
    [SerializeField] Transform objetivo;
    [Tooltip("A cuántas unidades de la peleadora se detiene")]
    [SerializeField] float distancia = 1.5f;

    [Header("Movimiento")]
    [Tooltip("Unidades por segundo hacia la izquierda o la derecha")]
    [SerializeField] float velocidad = 2.5f;
    [Tooltip("Unidades por segundo hacia el fondo o hacia el frente")]
    [SerializeField] float velocidadProfundidad = 1.5f;

    Rigidbody2D cuerpo;
    float lado;
    SpriteRenderer dibujo;
    Animator animador;

    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        dibujo = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();

        lado = 1;
        if (cuerpo.position.x < objetivo.position.x)
        {
            lado = -1;
        }
    }

    void FixedUpdate()
    {
        Vector2 destino = new Vector2(objetivo.position.x + lado * distancia, objetivo.position.y);

        Vector2 siguiente = cuerpo.position;
        siguiente.x = Mathf.MoveTowards(siguiente.x, destino.x, velocidad * Time.deltaTime);
        siguiente.y = Mathf.MoveTowards(siguiente.y, destino.y, velocidadProfundidad * Time.deltaTime);

        bool camina = siguiente != cuerpo.position;
        animador.SetBool("Caminando", camina);
        dibujo.flipX = objetivo.position.x > cuerpo.position.x;

        cuerpo.MovePosition(siguiente);
    }
}
