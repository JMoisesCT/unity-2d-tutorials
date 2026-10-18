using UnityEngine;
using UnityEngine.SceneManagement;

public class Puerta : MonoBehaviour
{
    [Tooltip("El Sprite Renderer del hijo Arriba")]
    [SerializeField] SpriteRenderer arriba;

    [Header("Puerta abierta")]
    [SerializeField] Sprite abiertaAbajo;
    [SerializeField] Sprite abiertaArriba;

    [Header("Salida")]
    [Tooltip("Nombre exacto de la escena que se carga")]
    [SerializeField] string siguienteNivel;

    bool abierta;

    public void Abrir()
    {
        GetComponent<SpriteRenderer>().sprite = abiertaAbajo;
        arriba.sprite = abiertaArriba;
        abierta = true;
    }

    public void Entrar()
    {
        if (abierta)
        {
            SceneManager.LoadScene(siguienteNivel);
        }
        else
        {
            Debug.Log("La puerta está cerrada.");
        }
    }
}
