using System.Collections;
using UnityEngine;

public class AnimarEscala : MonoBehaviour
{
    Coroutine actual;

    public void Ir(float destino, float segundos, AnimationCurve forma)
    {
        if (actual != null)
        {
            StopCoroutine(actual);
        }
        actual = StartCoroutine(Animar(destino, segundos, forma));
    }

    IEnumerator Animar(float destino, float segundos, AnimationCurve forma)
    {
        Vector3 inicio = transform.localScale;
        Vector3 fin = Vector3.one * destino;
        float tiempo = 0f;
        while (tiempo < segundos)
        {
            tiempo += Time.deltaTime;
            transform.localScale = Vector3.LerpUnclamped(inicio, fin, forma.Evaluate(tiempo / segundos));
            yield return null;
        }
    }
}
