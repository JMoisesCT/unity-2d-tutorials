using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Oleada", menuName = "Scriptable Objects/Oleada")]
public class Oleada : ScriptableObject
{
    [Tooltip("Lo que aparece, en orden")]
    public List<Paso> pasos;
}

[System.Serializable]
public class Paso
{
    [Tooltip("Segundos de espera desde el paso anterior")]
    public float espera = 1f;
    [Tooltip("El prefab que aparece")]
    public GameObject enemigo;
    [Tooltip("A qué altura entra por la derecha")]
    public float altura;
    [Tooltip("Recto o en seno (solo las naves)")]
    public Recorrido recorrido;
}

public enum Recorrido { Recto, Seno }