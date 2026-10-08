using UnityEngine;

public class Fruta : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            GameManager.instancia.SumarFruta();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Animator>().SetTrigger("Recoger");
            Destroy(gameObject, 0.3f);
        }
    }
}
