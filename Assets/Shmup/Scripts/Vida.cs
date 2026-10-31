using UnityEngine;

public class Vida : MonoBehaviour
{
    [Header("Vida")]
    [Tooltip("Golpes que aguanta")]
    [SerializeField] int vidaMaxima = 3;

    [Header("Al morir")]
    [Tooltip("El prefab que aparece en su lugar")]
    [SerializeField] GameObject explosion;
    [Tooltip("En qué se parte (si Pedazos es 0, en nada)")]
    [SerializeField] GameObject pedazo;
    [Tooltip("Cuántos pedazos")]
    [SerializeField] int pedazos = 0;

    int vida;

    void Start()
    {
        vida = vidaMaxima;
    }

    public void RecibirDaño(int daño)
    {
        vida -= daño;
        Debug.Log(name + " recibe " + daño + ". Le queda " + vida);
        if (vida <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            for (int i = 0; i < pedazos; i++)
            {
                Instantiate(pedazo, transform.position, Quaternion.identity, transform.parent);
            }
            Destroy(gameObject);
        }
    }
}
