using UnityEngine;

[CreateAssetMenu(fileName = "Curvas", menuName = "Scriptable Objects/Curvas")]
public class Curvas : ScriptableObject
{
    [Tooltip("Arranca despacio y frena al llegar.")]
    public AnimationCurve suave = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [Tooltip("Se pasa del destino y vuelve.")]
    public AnimationCurve rebote = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
}
