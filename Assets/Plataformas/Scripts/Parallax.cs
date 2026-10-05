using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float factor = 0.5f;

    Transform camara;
    float inicioX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camara = Camera.main.transform;
        inicioX = transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = inicioX + camara.position.x * factor;
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
