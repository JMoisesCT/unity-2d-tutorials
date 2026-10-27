using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    [Tooltip("La Image Filled que se achica")]
    [SerializeField] Image relleno;

    public void Mostrar(int vida, int maxima)
    {
        relleno.fillAmount = (float)vida / maxima;
    }
}
