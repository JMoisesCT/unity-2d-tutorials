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
}
