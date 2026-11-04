using UnityEngine;
using UnityEngine.SceneManagement;

public class Salida : MonoBehaviour
{
    [Tooltip("Nombre de la escena que se carga al llegar aquí")]
    [SerializeField] string escena;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.GetComponent<Rana>() != null)
        {
            SceneManager.LoadScene(escena);
        }
    }
}
