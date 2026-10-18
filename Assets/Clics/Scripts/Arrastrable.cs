using UnityEngine;

public class Arrastrable : MonoBehaviour
{
    [SerializeField] int ordenAgarrado = 10;

    SpriteRenderer dibujo;
    int ordenNormal;
    Vector3 inicio;

    void Start()
    {
        dibujo = GetComponent<SpriteRenderer>();
        ordenNormal = dibujo.sortingOrder;
    }

    public void Agarrar()
    {
        inicio = transform.position;
        dibujo.sortingOrder = ordenAgarrado;
    }

    public void Soltar()
    {
        dibujo.sortingOrder = ordenNormal;
    }

    public void Volver()
    {
        transform.position = inicio;
    }
}
