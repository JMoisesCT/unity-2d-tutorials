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

    public void RecibirDaño(int daño)
    {
        if (protegido)
        {
            return;
        }

        vida = vida - daño;
        Debug.Log(name + " recibe " + daño + " de daño. Le quedan " + vida);

        if (hud != null)
        {
            hud.Mostrar(vida);
        }

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Proteger());
        }
    }

    IEnumerator Proteger()
    {
        protegido = true;
        yield return new WaitForSeconds(invulnerable);
        protegido = false;
    }
}
