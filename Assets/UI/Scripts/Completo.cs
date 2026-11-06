using TMPro;
using UnityEngine;

public class Completo : MonoBehaviour
{
    [Tooltip("El texto que muestra el tiempo.")]
    [SerializeField] TMP_Text mensaje;
    [Tooltip("Las tres estrellas doradas, en orden.")]
    [SerializeField] Aparecer[] estrellas;
    [Tooltip("Cuántos segundos pasan de una estrella a la otra.")]
    [SerializeField] float separacion = 0.4f;

    Ventana ventana;

    void Awake()
    {
        ventana = GetComponent<Ventana>();
    }

    public void Mostrar(float tiempo, int ganadas)
    {
        int total = Mathf.FloorToInt(tiempo);
        int minutos = total / 60;
        int segundos = total % 60;
        mensaje.text = $"TIEMPO {minutos:00}:{segundos:00}";

        foreach (Aparecer estrella in estrellas)
        {
            estrella.transform.localScale = Vector3.zero;
        }
        for (int i = 0; i < ganadas; i++)
        {
            estrellas[i].Mostrar(0.5f + i * separacion);
        }
        ventana.Abrir();
    }
}
