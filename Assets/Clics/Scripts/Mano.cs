using UnityEngine;
using UnityEngine.InputSystem;

public class Mano : MonoBehaviour
{
    [SerializeField] float alturaMaxima = 0.5f;

    Arrastrable agarrado;
    Vector3 desfase;
    Vector3 anterior;
    Vector3 velocidad;
    float tiempo;

    void Update()
    {
        Vector2 pantalla = Mouse.current.position.ReadValue();
        Vector3 mundo = Camera.main.ScreenToWorldPoint(pantalla);
        mundo.z = 0;

        tiempo += Time.deltaTime;
        if (tiempo >= 0.05f)
        {
            velocidad = (mundo - anterior) / tiempo;
            anterior = mundo;
            tiempo = 0;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Collider2D tocado = Physics2D.OverlapPoint(mundo);
            if (tocado != null && tocado.TryGetComponent(out agarrado))
            {
                Debug.Log("Agarraste: " + agarrado.name);
                desfase = agarrado.transform.position - mundo;
                agarrado.Agarrar();
            }
        }

        if (agarrado != null && Mouse.current.leftButton.isPressed)
        {
            Vector3 destino = mundo + desfase;
            destino.y = Mathf.Min(destino.y, alturaMaxima);
            agarrado.Mover(destino);
        }

        if (agarrado != null && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            agarrado.Soltar(velocidad);
            agarrado = null;
        }
    }
}
