using UnityEngine;
using UnityEngine.Pool;

public class Laser : MonoBehaviour
{
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 15f;
    [Tooltip("Cuánta vida quita al chocar")]
    [SerializeField] int daño = 1;

    ObjectPool<Laser> cargador;

    public void UsarCargador(ObjectPool<Laser> deDonde)
    {
        cargador = deDonde;
    }

    void Update()
    {
        transform.Translate(Vector2.up * velocidad * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        if (gameObject.activeSelf)
        {
            cargador.Release(this);
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.TryGetComponent(out Vida vida))
        {
            vida.RecibirDaño(daño);
            cargador.Release(this);
        }
    }
}
