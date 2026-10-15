using UnityEngine;
using UnityEngine.SceneManagement;

public class Bandera : MonoBehaviour
{
    [Header("Meta")]
    [Tooltip("El nombre exacto de la escena que se carga al llegar")]
    [SerializeField] string siguienteNivel;
    [Tooltip("Segundos entre izar la bandera y cargar el otro nivel")]
    [SerializeField] float espera = 1f;

    [Header("Dibujo")]
    [Tooltip("La bandera izada, cuando llega el personaje")]
    [SerializeField] Sprite izada;

    float reloj;

    void Update()
    {
        if (reloj > 0f)
        {
            reloj -= Time.deltaTime;
            if (reloj <= 0f)
            {
                SceneManager.LoadScene(siguienteNivel);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        Personaje personaje = otro.GetComponent<Personaje>();
        if (personaje != null)
        {
            GetComponent<SpriteRenderer>().sprite = izada;
            reloj = espera;
        }
    }
}
