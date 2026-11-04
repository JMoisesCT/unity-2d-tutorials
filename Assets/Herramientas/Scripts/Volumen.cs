using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Volumen : MonoBehaviour
{
    [Tooltip("El Audio Mixer con el parámetro VolumenMusica")]
    [SerializeField] AudioMixer mezcla;
    [Tooltip("0 = mudo, 1 = volumen completo")]
    [Range(0, 1)]
    [SerializeField] float volumenMusica = 1;

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            volumenMusica = Mathf.Max(volumenMusica - 0.1f, 0);
            Debug.Log("Volumen de la música: " + volumenMusica);
        }
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            volumenMusica = Mathf.Min(volumenMusica + 0.1f, 1);
            Debug.Log("Volumen de la música: " + volumenMusica);
        }
        mezcla.SetFloat("VolumenMusica", Mathf.Log10(Mathf.Max(volumenMusica, 0.0001f)) * 20);
    }
}
