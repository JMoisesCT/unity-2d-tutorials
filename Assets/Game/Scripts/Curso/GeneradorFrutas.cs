using UnityEngine;
using System.Collections.Generic;

public class GeneradorFrutas : MonoBehaviour
{
    [SerializeField] GameObject prefabFruta;
    [SerializeField] Sprite[] dibujos;
    List<GameObject> frutas = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CrearFrutas();
    }

    // Update is called once per frame
    void Update()
    {
        if (ContarFrutas() == 0)
        {
            CrearFrutas();
        }
    }

    void CrearFrutas()
    {
        frutas.Clear();
        for (int i = 0; i < dibujos.Length; i++)
        {
            GameObject fruta = Instantiate(prefabFruta);
            float x = Random.Range(-7f, 7f);
            float y = Random.Range(-4f, 4f);
            fruta.transform.position = new Vector3(x, y, 0);
            fruta.GetComponent<SpriteRenderer>().sprite = dibujos[i];
            frutas.Add(fruta);
        }
        Debug.Log("Frutas nuevas: " + frutas.Count);
    }

    int ContarFrutas()
    {
        int quedan = 0;
        foreach (GameObject fruta in frutas)
        {
            if (fruta != null)
            {
                quedan = quedan + 1;
            }
        }
        return quedan;
    }
}
