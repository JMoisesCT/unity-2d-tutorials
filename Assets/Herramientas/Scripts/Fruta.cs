using UnityEngine;

public class Fruta : MonoBehaviour
{
    [Tooltip("Color de las estrellas cuando la rana la junta")]
    [SerializeField] Color color = Color.white;

    public Color ColorEstallido()
    {
        return color;
    }
}
