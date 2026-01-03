using UnityEngine;

public class Collect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //aggiorna il contatore, riproduce un suono e distrugge la moneta
            GameManager.Monete++;
            AudioManager.Instance.AudioList[4].Play();
            Destroy(gameObject);
        }
    }
}
