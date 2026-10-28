using System.Collections.Generic;
using UnityEngine;

public class Combatiente : MonoBehaviour
{
    [Tooltip("Pelea contra los del otro bando")]
    [SerializeField] Bando bando;

    static List<Combatiente> todos = new List<Combatiente>();

    SpriteRenderer dibujo;

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

    void Start()
    {
        dibujo = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Combatiente rival = RivalMasCercano();
        if (rival == null)
        {
            return;
        }

        dibujo.flipX = rival.transform.position.x < transform.position.x;
        Debug.DrawLine(transform.position, rival.transform.position, Color.red);
    }

    Combatiente RivalMasCercano()
    {
        Combatiente cercano = null;
        float menor = Mathf.Infinity;

        foreach (Combatiente otro in todos)
        {
            if (otro.bando != bando)
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
