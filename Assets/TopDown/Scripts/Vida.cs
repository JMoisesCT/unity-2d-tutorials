using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] int vida = 3;

    public void RecibirDaño(int daño)
    {
        vida = vida - daño;
        Debug.Log(name + " recibe " + daño + " de daño. Le quedan " + vida);

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
