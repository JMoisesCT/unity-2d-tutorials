using UnityEngine;

public class AtaqueDisparo : Ataque
{
    [Tooltip("El prefab que dispara: Flecha o Bola")]
    [SerializeField] Proyectil proyectil;

    public override void Atacar(DatosHeroe datos)
    {
        base.Atacar(datos);

        Vector2 direccion = new Vector2(transform.lossyScale.x, 0);
        Proyectil nuevo = Instantiate(proyectil, transform.position, Quaternion.identity);
        nuevo.Lanzar(direccion, datos.daño);
    }
}
