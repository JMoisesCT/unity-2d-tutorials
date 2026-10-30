using UnityEngine;

public class Fondo : MonoBehaviour
{
    [Header("Desplazamiento")]
    [Tooltip("Unidades por segundo hacia la izquierda")]
    [SerializeField] float velocidad = 1.5f;
    [Tooltip("Ancho de un tile del fondo: 256 píxeles / 100 = 2.56")]
    [SerializeField] float ancho = 2.56f;

    void Update()
    {
        transform.Translate(Vector2.left * velocidad * Time.deltaTime);

        if (transform.position.x <= -ancho)
        {
            transform.position += Vector3.right * ancho;
        }
    }
}
