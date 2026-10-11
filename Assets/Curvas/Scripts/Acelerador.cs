using UnityEngine;

public class Acelerador : MonoBehaviour
{
    [Tooltip("La velocidad de suelo que le da al personaje, hacia donde apunta la flecha")]
    [Range(5f, 25f)]
    [SerializeField] float velocidad = 15f;

    void OnTriggerEnter2D(Collider2D otro)
    {
        Personaje personaje = otro.GetComponent<Personaje>();
        if (personaje != null)
        {
            float direccion = Mathf.Sign(transform.localScale.x);
            personaje.Impulsar(velocidad * direccion);
        }
    }
}
