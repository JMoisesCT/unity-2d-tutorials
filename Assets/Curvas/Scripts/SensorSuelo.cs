using UnityEngine;

public class SensorSuelo : MonoBehaviour
{
    [Header("Rayo hacia el suelo")]
    [Tooltip("Hasta dónde mira hacia abajo, desde el centro del alien")]
    [SerializeField] float largoRayo = 1.5f;
    [Tooltip("Las capas que cuentan como suelo")]
    [SerializeField] LayerMask capaSuelo;

    Rigidbody2D cuerpo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D suelo = Physics2D.Raycast(transform.position, Vector2.down, largoRayo, capaSuelo);
        Debug.DrawRay(transform.position, Vector2.down * largoRayo, Color.white);

        if (suelo.collider != null)
        {
            Debug.DrawRay(suelo.point, suelo.normal, Color.yellow);

            Vector2 tangente = -Vector2.Perpendicular(suelo.normal);
            Debug.DrawRay(suelo.point, tangente, Color.cyan);

            float angulo = Vector2.SignedAngle(Vector2.up, suelo.normal);
            cuerpo.rotation = angulo;
        }
    }
}
