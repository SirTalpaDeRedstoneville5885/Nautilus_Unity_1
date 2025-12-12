using System.Threading;
using UnityEngine;

public class kill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            //    Debug.Log("Player Toccato!");
            PlayerMoves.Instance.Anim.SetTrigger("LostLife");
            collision.transform.position = Checkpoint.GetActiveCheckpoint();
            GameManager.Vite--;
        }
    }
}
