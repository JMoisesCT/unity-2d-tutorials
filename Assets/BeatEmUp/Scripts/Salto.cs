using UnityEngine;
using UnityEngine.InputSystem;

public class Salto : MonoBehaviour
{
    [Header("Salto")]
    [Tooltip("El hijo que sube y baja; la raíz y la sombra se quedan en el suelo")]
    [SerializeField] Transform cuerpo;
    [Tooltip("Velocidad hacia arriba al despegar, en unidades por segundo")]
    [SerializeField] float fuerza = 8f;
    [Tooltip("Cuánta velocidad hacia arriba pierde cada segundo")]
    [SerializeField] float gravedad = 20f;

    InputAction saltar;
    float altura;
    float velocidadVertical;

    void Start()
    {
        saltar = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        if (saltar.WasPressedThisFrame() && !EnElAire())
        {
            velocidadVertical = fuerza;
        }

        velocidadVertical -= gravedad * Time.deltaTime;
        altura += velocidadVertical * Time.deltaTime;
        if (altura < 0)
        {
            altura = 0;
            velocidadVertical = 0;
        }

        cuerpo.localPosition = new Vector3(0, altura, 0);
    }

    public bool EnElAire()
    {
        return altura > 0;
    }
}
