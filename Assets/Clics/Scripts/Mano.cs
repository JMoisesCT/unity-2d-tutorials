using UnityEngine;
using UnityEngine.InputSystem;

public class Mano : MonoBehaviour
{
    Arrastrable agarrado;
    Vector3 desfase;

    void Update()
    {
        Vector2 pantalla = Mouse.current.position.ReadValue();
        Vector3 mundo = Camera.main.ScreenToWorldPoint(pantalla);
        mundo.z = 0;

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
            agarrado.transform.position = mundo + desfase;
        }

        if (agarrado != null && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            agarrado.Soltar();
            agarrado = null;
        }
    }
}
