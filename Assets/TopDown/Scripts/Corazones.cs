using UnityEngine;
using UnityEngine.UI;

public class Corazones : MonoBehaviour
{
    [Header("Corazones")]
    [Tooltip("Las Image de los corazones, de izquierda a derecha")]
    [SerializeField] Image[] corazones;

    [Header("Dibujos")]
    [SerializeField] Sprite lleno;
    [SerializeField] Sprite medio;
    [SerializeField] Sprite vacio;

    public void Mostrar(int vida)
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (vida >= i * 2 + 2)
            {
                corazones[i].sprite = lleno;
            }
            else if (vida == i * 2 + 1)
            {
                corazones[i].sprite = medio;
            }
            else
            {
                corazones[i].sprite = vacio;
            }
        }
    }
}
