using System.Collections;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] int vida = 3;
    [Tooltip("Segundos sin recibir daño después de un golpe")]
    [SerializeField] float invulnerable = 1f;
    [Tooltip("Los corazones de la pantalla")]
    [SerializeField] Corazones hud;

    [Header("Botín")]
    [Tooltip("El prefab de la moneda que suelta al morir")]
    [SerializeField] Moneda moneda;
    [Tooltip("Cuántas monedas suelta")]
    [SerializeField] int monedas = 0;

    bool protegido;
    int vidaMaxima;

    public void Empezar(int maxima)
    {
        vidaMaxima = maxima;
        vida = maxima;
        AvisarHud();
    }

    public void CambiarMaxima(int maxima)
    {
        vida = Mathf.CeilToInt((float)vida / vidaMaxima * maxima);
        vidaMaxima = maxima;
        Debug.Log(name + " ahora tiene " + vida + " de " + maxima);
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
            SoltarMonedas();
            Sala sala = FindAnyObjectByType<Sala>();
            if (sala != null)
            {
                if (TryGetComponent(out Perseguidor enemigo))
                {
                    sala.EnemigoVencido();
                }
                else
                {
                    sala.HeroeVencido();
                }
            }
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Proteger());
        }
    }

    void SoltarMonedas()
    {
        for (int i = 0; i < monedas; i++)
        {
            Vector2 lugar = (Vector2)transform.position + Random.insideUnitCircle;
            Instantiate(moneda, lugar, Quaternion.identity);
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
