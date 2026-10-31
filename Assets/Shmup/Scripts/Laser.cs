using UnityEngine;
using UnityEngine.Pool;

public class Laser : MonoBehaviour
{
    [Tooltip("Unidades por segundo")]
    [SerializeField] float velocidad = 15f;

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
        cargador.Release(this);
    }
}
