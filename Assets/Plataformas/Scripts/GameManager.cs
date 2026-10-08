using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    public int vidas = 3;
    public int frutas = 0;

    void Awake()
    {
        if (instancia != null)
        {
            Destroy(gameObject);
            return;
        }
        instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SumarFruta()
    {
        frutas++;
    }

    public void PerderVida()
    {
        vidas--;
    }

    public void Reiniciar()
    {
        vidas = 3;
        frutas = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Nivel1");
    }
}
