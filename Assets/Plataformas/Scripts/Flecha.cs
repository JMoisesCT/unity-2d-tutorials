using UnityEngine;

public class Flecha : MonoBehaviour
{
    [SerializeField] float velocidad = 7f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.up * velocidad;
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.isTrigger == false)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            GetComponent<Animator>().Play("FlechaHit");
            Destroy(gameObject, 0.2f);
        }
    }
}
