using UnityEngine;

public class Candado : MonoBehaviour
{
    [SerializeField] ColorLlave color;
    [SerializeField] Nivel nivel;

    public bool Acepta(ColorLlave colorLlave)
    {
        return colorLlave == color;
    }

    public void Abrir()
    {
        nivel.CandadoAbierto();
        gameObject.SetActive(false);
    }
}
