using UnityEngine;

public class Ataque : MonoBehaviour
{
    public virtual void Atacar(DatosHeroe datos)
    {
        Debug.Log(datos.nombre + " ataca con " + name);
    }
}
