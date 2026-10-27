using UnityEngine;
using UnityEngine.InputSystem;

public class Combo : MonoBehaviour
{
    [Header("Combo")]
    [Tooltip("El Animator del hijo Cuerpo")]
    [SerializeField] Animator animador;
    [Tooltip("Segundos desde el golpe anterior para seguir el combo")]
    [SerializeField] float ventana = 0.8f;
    [Tooltip("Segundos que dura cada golpe; mientras tanto no se puede golpear otra vez")]
    [SerializeField] float duracion = 0.3f;

    [Header("Golpe")]
    [Tooltip("El Sprite Renderer del hijo Cuerpo, para saber hacia dónde mira")]
    [SerializeField] SpriteRenderer dibujo;
    [Tooltip("Centro de la caja del golpe, medido desde los pies, mirando a la derecha")]
    [SerializeField] Vector2 centroGolpe = new Vector2(1.6f, 2f);
    [Tooltip("Ancho y alto de la caja del golpe")]
    [SerializeField] Vector2 tamañoGolpe = new Vector2(1.2f, 0.6f);
    [Tooltip("Diferencia de profundidad máxima para que el golpe llegue")]
    [SerializeField] float carril = 0.4f;
    [Tooltip("Velocidad con la que el Jab y el Punch empujan al Punk")]
    [SerializeField] float empujon = 3f;
    [Tooltip("Velocidad con la que el Kick empuja al Punk")]
    [SerializeField] float empujonFuerte = 9f;
    [Tooltip("Vida que quitan el Jab y el Punch")]
    [SerializeField] int daño = 1;
    [Tooltip("Vida que quita el Kick")]
    [SerializeField] int dañoFuerte = 2;

    [Header("Patada voladora")]
    [Tooltip("Centro de la caja de la patada voladora, medido desde el Cuerpo, mirando a la derecha")]
    [SerializeField] Vector2 centroAereo = new Vector2(1.4f, 1f);
    [Tooltip("Ancho y alto de la caja de la patada voladora")]
    [SerializeField] Vector2 tamañoAereo = new Vector2(1.2f, 0.8f);
    [Tooltip("Velocidad con la que la patada voladora empuja al Punk")]
    [SerializeField] float empujonAereo = 6f;
    [Tooltip("Vida que quita la patada voladora")]
    [SerializeField] int dañoAereo = 2;

    InputAction atacar;
    int golpe;
    float ultimoGolpe;
    Salto salto;
    bool pateando;

    void Start()
    {
        atacar = InputSystem.actions.FindAction("Attack");
        salto = GetComponent<Salto>();
    }

    void Update()
    {
        if (!salto.EnElAire())
        {
            pateando = false;
        }

        if (atacar.WasPressedThisFrame() && salto.EnElAire())
        {
            pateando = true;
        }
        else if (atacar.WasPressedThisFrame() && !Golpeando())
        {
            if (Time.time - ultimoGolpe > ventana)
            {
                golpe = 0;
            }
            golpe++;
            if (golpe > 3)
            {
                golpe = 1;
            }
            ultimoGolpe = Time.time;
            Debug.Log("Golpe " + golpe);

            if (golpe == 1)
            {
                animador.SetTrigger("Jab");
            }
            else if (golpe == 2)
            {
                animador.SetTrigger("Punch");
            }
            else
            {
                animador.SetTrigger("Kick");
            }
        }

        animador.SetBool("Pateando", pateando);
    }

    public bool Golpeando()
    {
        return Time.time - ultimoGolpe < duracion;
    }

    public void Conectar()
    {
        Vector2 centro = CentroGolpe();
        Vector2 tamaño = tamañoGolpe;
        if (pateando)
        {
            centro = CentroAereo();
            tamaño = tamañoAereo;
        }

        Collider2D[] tocados = Physics2D.OverlapBoxAll(centro, tamaño, 0);
        foreach (Collider2D tocado in tocados)
        {
            IGolpeable golpeable = tocado.GetComponent<IGolpeable>();
            if (golpeable != null && Mathf.Abs(tocado.transform.position.y - transform.position.y) < carril)
            {
                Debug.Log("Le pega a " + tocado.name);
                float fuerza = empujon;
                int quita = daño;
                if (pateando)
                {
                    fuerza = empujonAereo;
                    quita = dañoAereo;
                }
                else if (golpe == 3)
                {
                    fuerza = empujonFuerte;
                    quita = dañoFuerte;
                }
                golpeable.Recibir(fuerza, Lado(), quita);
            }
        }
    }

    float Lado()
    {
        if (dibujo.flipX)
        {
            return -1;
        }
        return 1;
    }

    Vector2 CentroGolpe()
    {
        return (Vector2)transform.position + new Vector2(centroGolpe.x * Lado(), centroGolpe.y);
    }

    Vector2 CentroAereo()
    {
        return (Vector2)dibujo.transform.position + new Vector2(centroAereo.x * Lado(), centroAereo.y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(CentroGolpe(), tamañoGolpe);
        Gizmos.color = new Color(1, 0.6f, 0, 0.5f);
        Gizmos.DrawCube(CentroAereo(), tamañoAereo);
    }
}
