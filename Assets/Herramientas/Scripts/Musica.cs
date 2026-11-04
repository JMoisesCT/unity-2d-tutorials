using System.Collections;
using UnityEngine;

public class Musica : MonoBehaviour
{
    [Tooltip("Segundos que tarda en bajar y en subir al cambiar de canción")]
    [SerializeField] float fundido = 1;

    static Musica actual;
    AudioSource altavoz;

    void Awake()
    {
        altavoz = GetComponent<AudioSource>();
        if (actual != null)
        {
            actual.Cambiar(altavoz.clip);
            Destroy(gameObject);
            return;
        }
        actual = this;
        DontDestroyOnLoad(gameObject);
    }

    void Cambiar(AudioClip cancion)
    {
        if (altavoz.clip != cancion)
        {
            StartCoroutine(Fundir(cancion));
        }
    }

    IEnumerator Fundir(AudioClip cancion)
    {
        float volumen = altavoz.volume;
        for (float t = 0; t < fundido; t += Time.deltaTime)
        {
            altavoz.volume = Mathf.Lerp(volumen, 0, t / fundido);
            yield return null;
        }
        altavoz.clip = cancion;
        altavoz.Play();
        for (float t = 0; t < fundido; t += Time.deltaTime)
        {
            altavoz.volume = Mathf.Lerp(0, volumen, t / fundido);
            yield return null;
        }
        altavoz.volume = volumen;
    }
}
