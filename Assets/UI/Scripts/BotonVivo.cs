using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonVivo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Al pasar el mouse")]
    [Tooltip("Cuánto crece: 1 es su tamaño normal.")]
    [SerializeField] float crecer = 1.05f;
    [Header("Sonidos")]
    [Tooltip("Suena cuando el mouse entra.")]
    [SerializeField] AudioClip sonidoEncima;
    [Tooltip("Suena al pulsar.")]
    [SerializeField] AudioClip sonidoClic;

    Button boton;
    AudioSource fuente;

    void Awake()
    {
        boton = GetComponent<Button>();
        fuente = GetComponent<AudioSource>();
        boton.onClick.AddListener(SonarClic);
    }

    public void OnPointerEnter(PointerEventData datos)
    {
        if (!boton.IsInteractable())
        {
            return;
        }
        transform.localScale = Vector3.one * crecer;
        fuente.PlayOneShot(sonidoEncima);
    }

    public void OnPointerExit(PointerEventData datos)
    {
        transform.localScale = Vector3.one;
    }

    void SonarClic()
    {
        fuente.PlayOneShot(sonidoClic);
    }
}
