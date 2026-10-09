using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Cronometro : MonoBehaviour
{
    [SerializeField] TMP_Text textoFinal;

    TMP_Text texto;
    float tiempo;
    bool corriendo = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texto = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (corriendo)
        {
            tiempo += Time.deltaTime;
            texto.text = tiempo.ToString("F1");
        }
    }

    public void Detener()
    {
        corriendo = false;
        string clave = SceneManager.GetActiveScene().name;
        float record = PlayerPrefs.GetFloat(clave, 9999f);
        if (tiempo < record)
        {
            record = tiempo;
            PlayerPrefs.SetFloat(clave, record);
        }
        textoFinal.text = "Tiempo: " + tiempo.ToString("F1") + " s";
        textoFinal.text += "\nRécord: " + record.ToString("F1") + " s";
    }
}
