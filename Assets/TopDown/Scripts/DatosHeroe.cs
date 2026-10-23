using UnityEngine;

[CreateAssetMenu(fileName = "DatosHeroe", menuName = "Scriptable Objects/DatosHeroe")]
public class DatosHeroe : ScriptableObject
{
    [Header("Ficha")]
    public string nombre = "Caballero";
    [Tooltip("Puntos de vida: cada corazón vale 2")]
    public int vida = 6;
    [Tooltip("Unidades por segundo")]
    public float velocidad = 4.5f;

    [Header("Ataque")]
    [Tooltip("Vida que quita cada golpe")]
    public int daño = 1;
    [Tooltip("Radio del círculo del golpe")]
    public float alcance = 0.8f;
    [Tooltip("Segundos entre un golpe y el siguiente")]
    public float cadencia = 0.4f;

    [Header("Dibujo")]
    [Tooltip("Las animaciones del héroe: un Animator Controller o un Override")]
    public RuntimeAnimatorController animaciones;
}
