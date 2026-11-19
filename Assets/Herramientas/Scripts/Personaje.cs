using UnityEngine;

[CreateAssetMenu(fileName = "Personaje", menuName = "Scriptable Objects/Personaje")]
public class Personaje : ScriptableObject
{
    [Tooltip("El nombre que sale en la etiqueta")]
    public string nombre;
    [Tooltip("La cara que se ve en la caja")]
    public Sprite retrato;
    [Tooltip("El color de la etiqueta con el nombre")]
    public Color color = Color.white;
    [Tooltip("1 = voz normal; más alto, más aguda")]
    [Range(0.5f, 2f)]
    public float tono = 1f;
}
