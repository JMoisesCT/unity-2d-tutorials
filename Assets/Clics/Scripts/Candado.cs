using UnityEngine;

public class Candado : MonoBehaviour
{
    [SerializeField] ColorLlave color;
    [SerializeField] Nivel nivel;

    [Header("Estrellas")]
    [Tooltip("El prefab de partículas que sale al abrir")]
    [SerializeField] ParticleSystem estrellas;
    [Tooltip("Un color por cada valor de ColorLlave, en el mismo orden")]
    [SerializeField] Color[] colores;

    public bool Acepta(ColorLlave colorLlave)
    {
        return colorLlave == color;
    }

    public void Abrir()
    {
        ParticleSystem efecto = Instantiate(estrellas, transform.position, Quaternion.identity);
        ParticleSystem.MainModule principal = efecto.main;
        principal.startColor = colores[(int)color];

        nivel.CandadoAbierto();
        gameObject.SetActive(false);
    }
}
