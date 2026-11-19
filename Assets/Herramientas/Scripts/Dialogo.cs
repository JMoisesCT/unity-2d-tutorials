using UnityEngine;

[System.Serializable]
public class Linea
{
    public Personaje quien;
    [TextArea(2, 3)]
    public string texto;
}

[System.Serializable]
public class Opcion
{
    [Tooltip("Lo que dice el botón")]
    public string texto;
    [Tooltip("El diálogo que sigue si eliges esta respuesta")]
    public Dialogo siguiente;
}

[CreateAssetMenu(fileName = "Dialogo", menuName = "Scriptable Objects/Dialogo")]
public class Dialogo : ScriptableObject
{
    public Linea[] lineas;
    [Tooltip("Respuestas al final; si está vacío, la charla termina")]
    public Opcion[] opciones;
}
