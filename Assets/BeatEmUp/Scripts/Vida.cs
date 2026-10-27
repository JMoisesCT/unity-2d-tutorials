using UnityEngine;

public class Vida : MonoBehaviour
{
    [Tooltip("Vida al empezar")]
    [SerializeField] int vidaMaxima = 6;

    int vida;

    void Start()
    {
        vida = vidaMaxima;
    }

    public void RecibirDaño(int daño)
    {
        vida -= daño;
        Debug.Log(name + " tiene " + vida + " de vida");
    }

    public bool Vivo()
    {
        return vida > 0;
    }
}
