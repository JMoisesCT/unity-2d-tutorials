using UnityEngine;
using UnityEngine.UI;

public class BarraFrutas : MonoBehaviour
{
    [Header("Imágenes que se llenan")]
    [Tooltip("La imagen Filled que se llena con las frutas.")]
    [SerializeField] Image relleno;
    [Tooltip("La estrella que se llena en círculo.")]
    [SerializeField] Image estrella;

    public void Mostrar(int frutas, int total)
    {
        float parte = (float)frutas / total;
        relleno.fillAmount = parte;
        estrella.fillAmount = parte;
    }
}
