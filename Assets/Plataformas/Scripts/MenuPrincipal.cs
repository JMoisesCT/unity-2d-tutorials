using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] TMP_Text textoRecord1;
    [SerializeField] TMP_Text textoRecord2;
    [SerializeField] TMP_Text textoRecord3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.instancia != null)
        {
            Destroy(GameManager.instancia.gameObject);
        }
        MostrarRecord(textoRecord1, "Nivel1");
        MostrarRecord(textoRecord2, "Nivel2");
        MostrarRecord(textoRecord3, "Nivel3");
    }

    void MostrarRecord(TMP_Text texto, string nivel)
    {
        float record = PlayerPrefs.GetFloat(nivel, 0f);
        if (record == 0f)
        {
            texto.text = nivel + ": sin récord";
        }
        else
        {
            texto.text = nivel + ": " + record.ToString("F1") + " s";
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Nivel1");
    }
}
