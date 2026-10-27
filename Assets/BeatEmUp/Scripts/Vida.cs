using UnityEngine;

public class Vida : MonoBehaviour
{
    [Tooltip("Vida al empezar")]
    [SerializeField] int vidaMaxima = 6;
    [Tooltip("Segundos sin recibir daño después de un golpe")]
    [SerializeField] float invulnerable = 0f;

    int vida;
    float protegidaHasta;

    void Start()
    {
        vida = vidaMaxima;
    }

    public bool RecibirDaño(int daño)
    {
        if (Protegida())
        {
            return false;
        }
        vida -= daño;
        protegidaHasta = Time.time + invulnerable;
        Debug.Log(name + " tiene " + vida + " de vida");
        return true;
    }

    public bool Protegida()
    {
        return Time.time < protegidaHasta;
    }

    public bool Vivo()
    {
        return vida > 0;
    }
}
