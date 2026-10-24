using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sala : MonoBehaviour
{
    [Tooltip("Se abre al vencer a todos los enemigos")]
    [SerializeField] Puerta puerta;
    [Tooltip("Segundos antes de volver a empezar")]
    [SerializeField] float espera = 1f;

    int enemigos;

    void Start()
    {
        enemigos = FindObjectsByType<Perseguidor>(FindObjectsSortMode.None).Length;
        Debug.Log("Enemigos en la sala: " + enemigos);
    }

    public void EnemigoVencido()
    {
        enemigos = enemigos - 1;
        Debug.Log("Quedan " + enemigos + " enemigos");
        if (enemigos == 0)
        {
            puerta.Abrir();
        }
    }

    public void HeroeVencido()
    {
        Debug.Log("El héroe cayó. La sala vuelve a empezar.");
        StartCoroutine(Reiniciar());
    }

    IEnumerator Reiniciar()
    {
        yield return new WaitForSeconds(espera);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
