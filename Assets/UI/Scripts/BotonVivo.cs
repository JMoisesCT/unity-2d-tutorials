using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotonVivo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Al pasar el mouse")]
    [Tooltip("Cuánto crece: 1 es su tamaño normal.")]
    [SerializeField] float crecer = 1.05f;
    [Tooltip("Cuántos segundos tarda en crecer.")]
    [SerializeField] float duracion = 0.15f;
    [Tooltip("El asset con las curvas de toda la interfaz.")]
    [SerializeField] Curvas curvas;
    [Tooltip("Cuántos segundos dura el rebote al pulsar.")]
    [SerializeField] float duracionRebote = 0.5f;
    [Header("Sonidos")]
    [Tooltip("Suena cuando el mouse entra.")]
    [SerializeField] AudioClip sonidoEncima;
    [Tooltip("Suena al pulsar.")]
    [SerializeField] AudioClip sonidoClic;

    Button boton;
    AudioSource fuente;
    AnimarEscala escala;

    void Awake()
    {
        boton = GetComponent<Button>();
        fuente = GetComponent<AudioSource>();
        escala = GetComponent<AnimarEscala>();
        boton.onClick.AddListener(SonarClic);
    }

    public void OnPointerEnter(PointerEventData datos)
    {
        if (!boton.IsInteractable())
        {
            return;
        }
        escala.Ir(crecer, duracion, curvas.suave);
        fuente.PlayOneShot(sonidoEncima);
    }

    public void OnPointerExit(PointerEventData datos)
    {
        escala.Ir(1f, duracion, curvas.suave);
    }

    void SonarClic()
    {
        fuente.PlayOneShot(sonidoClic);
        transform.localScale = Vector3.one * 0.8f;
        escala.Ir(crecer, duracionRebote, curvas.rebote);
    }
}
