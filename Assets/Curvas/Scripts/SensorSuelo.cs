using UnityEngine;

public class SensorSuelo : MonoBehaviour
{
    [Header("Rayo hacia el suelo")]
    [Tooltip("Hasta dónde mira hacia abajo, desde el centro del alien")]
    [SerializeField] float largoRayo = 1.5f;
    [Tooltip("Las capas que cuentan como suelo")]
    [SerializeField] LayerMask capaSuelo;

    [Header("Lo que lee (para otros scripts)")]
    public bool enSuelo;
    public Vector2 tangente;
    public float angulo;

    Rigidbody2D cuerpo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D suelo = Physics2D.Raycast(transform.position, -transform.up, largoRayo, capaSuelo);
        Debug.DrawRay(transform.position, -transform.up * largoRayo, Color.white);
        enSuelo = suelo.collider != null;

        if (enSuelo)
        {
            Debug.DrawRay(suelo.point, suelo.normal, Color.yellow);

            tangente = -Vector2.Perpendicular(suelo.normal);
            Debug.DrawRay(suelo.point, tangente, Color.cyan);

            angulo = Vector2.SignedAngle(Vector2.up, suelo.normal);
            cuerpo.rotation = angulo;
        }
    }
}
