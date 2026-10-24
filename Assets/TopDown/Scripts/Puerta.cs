using UnityEngine;

public class Puerta : MonoBehaviour
{
    [Tooltip("El dibujo de la puerta abierta")]
    [SerializeField] Sprite dibujoAbierta;
    [Tooltip("El collider que no deja pasar mientras está cerrada")]
    [SerializeField] Collider2D bloqueo;

    bool abierta;

    public void Abrir()
    {
        GetComponent<SpriteRenderer>().sprite = dibujoAbierta;
        bloqueo.enabled = false;
        abierta = true;
        Debug.Log("¡La puerta se abrió!");
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (abierta && otro.TryGetComponent(out Heroe heroe))
        {
            Debug.Log("¡Sala superada!");
        }
    }
}
