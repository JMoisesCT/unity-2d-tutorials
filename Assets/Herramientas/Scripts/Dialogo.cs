using UnityEngine;

[System.Serializable]
public class Linea
{
    public Personaje quien;
    [TextArea(2, 3)]
    public string texto;
}

[CreateAssetMenu(fileName = "Dialogo", menuName = "Scriptable Objects/Dialogo")]
public class Dialogo : ScriptableObject
{
    public Linea[] lineas;
}
