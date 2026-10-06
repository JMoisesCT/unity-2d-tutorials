using System.Collections;
using UnityEngine;

public class Fuego : MonoBehaviour
{
    [SerializeField] float tiempoApagado = 2f;
    [SerializeField] float tiempoEncendido = 1.5f;

    Animator animador;
    Collider2D zona;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animador = GetComponent<Animator>();
        zona = GetComponent<Collider2D>();
        StartCoroutine(Ciclo());
    }

    IEnumerator Ciclo()
    {
        while (true)
        {
            animador.SetBool("Encendido", false);
            zona.enabled = false;
            yield return new WaitForSeconds(tiempoApagado);
            animador.SetBool("Encendido", true);
            zona.enabled = true;
            yield return new WaitForSeconds(tiempoEncendido);
        }
    }
}
