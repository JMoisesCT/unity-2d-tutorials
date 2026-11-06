using UnityEngine;

public class CambiarEscena : MonoBehaviour
{
    [Tooltip("El nombre de la escena, tal como está en el Project.")]
    [SerializeField] string escena = "Menu";

    public void Ir()
    {
        Fundido.actual.Ir(escena);
    }
}
