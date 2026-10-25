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
        Collider2D[] tocados = Physics2D.OverlapBoxAll(CentroGolpe(), tamañoGolpe, 0);
        foreach (Collider2D tocado in tocados)
        {
            Punk punk = tocado.GetComponent<Punk>();
            if (punk != null && Mathf.Abs(punk.transform.position.y - transform.position.y) < carril)
            {
                Debug.Log("Le pega a " + punk.name);
                float fuerza = empujon;
                if (golpe == 3)
                {
                    fuerza = empujonFuerte;
                }
                punk.Recibir(fuerza, Lado());
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(CentroGolpe(), tamañoGolpe);
    }
}
