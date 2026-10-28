using UnityEngine;

public class Ataque : MonoBehaviour
{
    public virtual void Atacar(DatosHeroe datos)
    {
        Debug.Log(datos.nombre + " ataca con " + name);
    }

    public bool PuedeGolpear(Collider2D tocado)
    {
        Combatiente mio = GetComponentInParent<Combatiente>();
        if (mio != null && tocado.TryGetComponent(out Combatiente suyo))
        {
            return mio.EsRival(suyo);
        }
        return tocado.transform != transform.root;
    }
}
