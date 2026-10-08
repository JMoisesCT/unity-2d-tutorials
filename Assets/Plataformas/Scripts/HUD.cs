using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] TMP_Text textoVidas;
    [SerializeField] TMP_Text textoFrutas;
    [SerializeField] GameObject panelGameOver;

    // Update is called once per frame
    void Update()
    {
        textoVidas.text = "Vidas: " + GameManager.instancia.vidas;
        textoFrutas.text = "Frutas: " + GameManager.instancia.frutas;
    }

    public void MostrarGameOver()
    {
        panelGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void VolverAEmpezar()
    {
        GameManager.instancia.Reiniciar();
    }
}
