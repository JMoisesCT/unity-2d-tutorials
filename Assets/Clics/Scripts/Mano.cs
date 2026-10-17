using UnityEngine;
using UnityEngine.InputSystem;

public class Mano : MonoBehaviour
{
    [SerializeField] Transform llave;

    void Update()
    {
        Vector2 pantalla = Mouse.current.position.ReadValue();
        Vector3 mundo = Camera.main.ScreenToWorldPoint(pantalla);
        mundo.z = 0;
        llave.position = mundo;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Píxeles: " + pantalla);
            Debug.Log("Unidades: " + mundo);
        }
    }
}
