using UnityEngine;

public class FondoMovil : MonoBehaviour
{
    [SerializeField] float velocidad = 1f;
    [SerializeField] float altoBaldosa = 4f;

    Vector3 inicio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inicio = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * velocidad * Time.deltaTime);

        if (transform.position.y < inicio.y - altoBaldosa)
        {
            transform.position = inicio;
        }
    }
}
