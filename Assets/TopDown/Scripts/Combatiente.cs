using System.Collections.Generic;
using UnityEngine;

public class Combatiente : MonoBehaviour
{
    [Tooltip("Pelea contra los del otro bando")]
    [SerializeField] Bando bando;

    static List<Combatiente> todos = new List<Combatiente>();

    void OnEnable()
    {
        todos.Add(this);
        Debug.Log(name + " entra. Combatientes: " + todos.Count);
    }

    void OnDisable()
    {
        todos.Remove(this);
        Debug.Log(name + " sale. Combatientes: " + todos.Count);
    }

    public bool EsRival(Combatiente otro)
    {
        return otro.bando != bando;
    }

    public List<Combatiente> Rivales()
    {
        List<Combatiente> rivales = new List<Combatiente>();

        foreach (Combatiente otro in todos)
        {
            if (EsRival(otro))
            {
                rivales.Add(otro);
            }
        }

        return rivales;
    }
}
