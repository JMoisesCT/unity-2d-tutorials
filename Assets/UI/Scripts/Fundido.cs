using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fundido : MonoBehaviour
{
    public static Fundido actual;

    [Tooltip("El Canvas Group de la pantalla negra.")]
    [SerializeField] CanvasGroup grupo;
    [Tooltip("Cuántos segundos dura cada mitad del fundido.")]
    [SerializeField] float duracion = 0.4f;
    [Tooltip("El asset con las curvas de toda la interfaz.")]
    [SerializeField] Curvas curvas;

    void Awake()
    {
        if (actual != null)
        {
            Destroy(gameObject);
            return;
        }
        actual = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Ir(string escena)
    {
        StartCoroutine(Cambiar(escena));
    }

    IEnumerator Cambiar(string escena)
    {
        grupo.blocksRaycasts = true;
        yield return Fundir(0f, 1f);
        SceneManager.LoadScene(escena);
        yield return null;
        yield return Fundir(1f, 0f);
        grupo.blocksRaycasts = false;
    }

    IEnumerator Fundir(float desde, float hasta)
    {
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float parte = curvas.suave.Evaluate(tiempo / duracion);
            grupo.alpha = Mathf.Lerp(desde, hasta, parte);
            yield return null;
        }
    }
}
