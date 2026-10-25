using UnityEngine;

public class AvisoGolpe : MonoBehaviour
{
    Combo combo;

    void Start()
    {
        combo = GetComponentInParent<Combo>();
    }

    // La llama el Animation Event de Jab, Punch y Kick
    public void Conectar()
    {
        combo.Conectar();
    }
}
