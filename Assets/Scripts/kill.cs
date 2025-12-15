using System.Threading;
using UnityEngine;
using System.Collections;

public class kill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            PlayerMoves.Anim.SetTrigger("LostLife");
            collision.transform.position = Checkpoint.GetActiveCheckpoint();
            GameManager.Vite--;
            StartCoroutine(collision.gameObject.GetComponent<PlayerMoves>().NonMuoversi());
        }
    }
}
