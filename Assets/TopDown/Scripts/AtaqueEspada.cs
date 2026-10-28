using UnityEngine;

public class AtaqueEspada : Ataque
{
    [Tooltip("Centro del círculo del golpe")]
    [SerializeField] Transform puntoGolpe;

    Animator animador;

    void Start()
    {
        animador = GetComponent<Animator>();
    }

    public override void Atacar(DatosHeroe datos)
    {
        base.Atacar(datos);
        animador.SetTrigger("Atacar");

        Collider2D[] tocados = Physics2D.OverlapCircleAll(puntoGolpe.position, datos.alcance);
        foreach (Collider2D tocado in tocados)
        {
            if (PuedeGolpear(tocado) && tocado.TryGetComponent(out Vida vida))
            {
                vida.RecibirDaño(datos.daño);
            }
        }
    }
}
