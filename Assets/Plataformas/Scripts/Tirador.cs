using System.Collections;
using UnityEngine;

public class Tirador : Enemigo
{
    [SerializeField] GameObject flecha;
    [SerializeField] LayerMask capaRana;
    [SerializeField] float distancia = 6f;
    [SerializeField] float espera = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        StartCoroutine(Vigilar());
    }

    IEnumerator Vigilar()
    {
        while (enabled)
        {
            Vector2 adelante = new Vector2(direccion, 0f);
            RaycastHit2D vista = Physics2D.Raycast(transform.position, adelante, distancia, capaRana);
            if (vista.collider != null)
            {
                Instantiate(flecha, transform.position, Quaternion.Euler(0f, 0f, -90f * direccion));
            }
            else
            {
                Girar();
            }
            yield return new WaitForSeconds(espera);
        }
    }

    void Update()
    {
        Debug.DrawRay(transform.position, new Vector2(direccion, 0f) * distancia, Color.red);
    }
}
