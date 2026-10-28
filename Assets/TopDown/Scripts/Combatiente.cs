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

    public Combatiente RivalMasCercano()
    {
        Combatiente cercano = null;
        float menor = Mathf.Infinity;

        foreach (Combatiente otro in todos)
        {
            if (EsRival(otro))
            {
                float distancia = Vector2.Distance(transform.position, otro.transform.position);
                if (distancia < menor)
                {
                    menor = distancia;
                    cercano = otro;
                }
            }
        }

        return cercano;
    }
}
