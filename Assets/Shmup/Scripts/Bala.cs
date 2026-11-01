using UnityEngine;
using UnityEngine.Pool;

public class Bala : MonoBehaviour
{
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 4f;

    ObjectPool<Bala> cargador;

    public void UsarCargador(ObjectPool<Bala> deDonde)
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
        if (otro.TryGetComponent(out Nave nave))
        {
            nave.RecibirGolpe();
            cargador.Release(this);
        }
    }
}