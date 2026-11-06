using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpciones : MonoBehaviour
{
    [Header("Controles")]
    [Tooltip("El slider del volumen.")]
    [SerializeField] Slider volumen;
    [Tooltip("El interruptor de los puntos que flotan.")]
    [SerializeField] Toggle puntos;
    [Tooltip("La lista de dificultades.")]
    [SerializeField] TMP_Dropdown dificultad;

    void Start()
    {
        volumen.SetValueWithoutNotify(PlayerPrefs.GetFloat(Claves.Volumen, 0.8f));
        puntos.SetIsOnWithoutNotify(PlayerPrefs.GetInt(Claves.Puntos, 1) == 1);
        dificultad.SetValueWithoutNotify(PlayerPrefs.GetInt(Claves.Dificultad, 1));
        AudioListener.volume = volumen.value;

        volumen.onValueChanged.AddListener(CambiarVolumen);
        puntos.onValueChanged.AddListener(CambiarPuntos);
        dificultad.onValueChanged.AddListener(CambiarDificultad);
    }

    void CambiarVolumen(float valor)
    {
        AudioListener.volume = valor;
        Guardar();
    }

    void CambiarPuntos(bool valor)
    {
        Guardar();
    }

    void CambiarDificultad(int valor)
    {
        Guardar();
    }

    void Guardar()
    {
        PlayerPrefs.SetFloat(Claves.Volumen, volumen.value);

        int numero = 0;
        if (puntos.isOn)
        {
            numero = 1;
        }
        PlayerPrefs.SetInt(Claves.Puntos, numero);

        PlayerPrefs.SetInt(Claves.Dificultad, dificultad.value);
    }
}
