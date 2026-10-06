using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meta : MonoBehaviour
{
    [SerializeField] Cronometro cronometro;
    [SerializeField] GameObject panelFinal;

    Animator animador;
    bool alcanzada;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animador = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player") && alcanzada == false)
        {
            alcanzada = true;
            cronometro.Detener();
            animador.SetTrigger("Llegar");
            StartCoroutine(Terminar());
        }
    }

    IEnumerator Terminar()
    {
        yield return new WaitForSeconds(1f);
        panelFinal.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Nivel1");
    }
}
