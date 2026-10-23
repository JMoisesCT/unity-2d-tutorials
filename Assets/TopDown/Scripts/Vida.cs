using System.Collections;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] int vida = 3;
    [Tooltip("Segundos sin recibir daño después de un golpe")]
    [SerializeField] float invulnerable = 1f;
    [Tooltip("Los corazones de la pantalla")]
    [SerializeField] Corazones hud;

    bool protegido;
    int vidaMaxima;

    public void Empezar(int maxima)
    {
        vidaMaxima = maxima;
        vida = maxima;
        AvisarHud();
    }

    public void RecibirDaño(int daño)
    {
        if (protegido)
        {
            return;
        }

        vida = vida - daño;
        Debug.Log(name + " recibe " + daño + " de daño. Le quedan " + vida);
        AvisarHud();

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Proteger());
        }
    }

    void AvisarHud()
    {
        if (hud != null)
        {
            hud.Mostrar(vida, vidaMaxima);
        }
    }

    IEnumerator Proteger()
    {
        protegido = true;
        yield return new WaitForSeconds(invulnerable);
        protegido = false;
    }
}
