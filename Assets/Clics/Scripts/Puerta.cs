using UnityEngine;

public class Puerta : MonoBehaviour
{
    [Tooltip("El Sprite Renderer del hijo Arriba")]
    [SerializeField] SpriteRenderer arriba;

    [Header("Puerta abierta")]
    [SerializeField] Sprite abiertaAbajo;
    [SerializeField] Sprite abiertaArriba;

    public void Abrir()
    {
        GetComponent<SpriteRenderer>().sprite = abiertaAbajo;
        arriba.sprite = abiertaArriba;
    }
}
