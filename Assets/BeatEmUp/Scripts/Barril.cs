using System.Collections;
using UnityEngine;

public class Barril : MonoBehaviour, IGolpeable
{
    [Header("Romperse")]
    [Tooltip("El dibujo del barril aplastado")]
    [SerializeField] Sprite aplastado;
    [Tooltip("Segundos que queda aplastado antes de parpadear")]
    [SerializeField] float tendido = 1f;
    [Tooltip("Cuántas veces parpadea antes de desaparecer")]
    [SerializeField] int parpadeos = 5;

    SpriteRenderer dibujo;
    Vida vida;

    void Start()
    {
        dibujo = GetComponent<SpriteRenderer>();
        vida = GetComponent<Vida>();
    }

    public void Recibir(float fuerza, float hacia, int daño)
    {
        vida.RecibirDaño(daño);
        if (!vida.Vivo())
        {
            StartCoroutine(Romperse());
        }
    }

    IEnumerator Romperse()
    {
        Debug.Log(name + " se rompe");
        foreach (Collider2D caja in GetComponents<Collider2D>())
        {
            caja.enabled = false;
        }
        dibujo.sprite = aplastado;
        yield return new WaitForSeconds(tendido);

        for (int i = 0; i < parpadeos; i++)
        {
            dibujo.enabled = false;
            yield return new WaitForSeconds(0.1f);
            dibujo.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
