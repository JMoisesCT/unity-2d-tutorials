using UnityEngine;

public class Empujon : MonoBehaviour
{
    [SerializeField] float velocidadInicial = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody2D cuerpo = GetComponent<Rigidbody2D>();
        cuerpo.linearVelocity = new Vector2(velocidadInicial, 0f);
    }
}
