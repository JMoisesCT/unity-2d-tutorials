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

    [Header("Interés")]
    [Tooltip("Puntos que vale un rival del otro bando")]
    public float odio = 8f;
    [Tooltip("Puntos que vale cada moneda del goblin")]
    public float codicia = 1f;

    [Header("Memoria")]
    [Tooltip("Segundos que sigue a un rival que dejó de ver")]
    public float memoria = 2f;

    [Header("Persecución")]
    [Tooltip("Parte de su velocidad con la que persigue (1 = toda)")]
    [Range(0, 1)]
    public float prisa = 0.7f;
}
