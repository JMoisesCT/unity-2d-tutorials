using UnityEngine;

public class CambioCapa : MonoBehaviour
{
    [Header("Las dos mitades del loop")]
    [Tooltip("La mitad de la derecha (empieza en la capa Suelo)")]
    [SerializeField] GameObject mitadDerecha;
    [Tooltip("La mitad de la izquierda (empieza en la capa Apagado)")]
    [SerializeField] GameObject mitadIzquierda;

    void OnTriggerEnter2D(Collider2D otro)
    {
        int suelo = LayerMask.NameToLayer("Suelo");
        int apagado = LayerMask.NameToLayer("Apagado");

        if (mitadDerecha.layer == suelo)
        {
            mitadDerecha.layer = apagado;
            mitadIzquierda.layer = suelo;
        }
        else
        {
            mitadDerecha.layer = suelo;
            mitadIzquierda.layer = apagado;
        }
    }
}
