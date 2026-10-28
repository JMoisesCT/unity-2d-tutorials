using UnityEngine;

[CreateAssetMenu(fileName = "PerfilIA", menuName = "Scriptable Objects/PerfilIA")]
public class PerfilIA : ScriptableObject
{
    [Header("Vista")]
    [Tooltip("Hasta cuántas unidades ve")]
    public float radio = 6f;
    [Tooltip("Ancho de la mirada en grados (360 = ve hacia todos lados)")]
    [Range(0, 360)]
    public float cono = 120f;
    [Tooltip("Las Layers que le tapan la vista")]
    public LayerMask muros;
}
