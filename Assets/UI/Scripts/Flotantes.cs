using UnityEngine;

public class Flotantes : MonoBehaviour
{
    [Tooltip("La rana que avisa cuando junta una fruta.")]
    [SerializeField] Rana rana;
    [Tooltip("El prefab del texto que flota.")]
    [SerializeField] PuntoFlotante punto;

    void OnEnable()
    {
        rana.FrutaJuntada += Crear;
    }

    void OnDisable()
    {
        rana.FrutaJuntada -= Crear;
    }

    void Crear(Vector3 lugar, int ganados)
    {
        if (PlayerPrefs.GetInt(Claves.Puntos, 1) == 0)
        {
            return;
        }
        PuntoFlotante nuevo = Instantiate(punto, transform);
        nuevo.transform.position = Camera.main.WorldToScreenPoint(lugar);
        nuevo.Mostrar(ganados);
    }
}
