using UnityEngine;

public class PuntoControl : MonoBehaviour
{
    bool activado;

    void OnTriggerEnter2D(Collider2D otro)
    {
        GolpeRana rana = otro.GetComponent<GolpeRana>();
        if (rana != null && activado == false)
        {
            activado = true;
            rana.GuardarPunto(transform.position);
            GetComponent<Animator>().SetTrigger("Activar");
        }
    }
}
