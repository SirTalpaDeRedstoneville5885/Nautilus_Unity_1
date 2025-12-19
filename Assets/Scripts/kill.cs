using System.Threading;
using UnityEngine;
using System.Collections;

public class kill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            collision.transform.position = Checkpoint.GetActiveCheckpoint();
            GameManager.Vite--;
            PlayerMoves.Anim.SetTrigger("LostLife");
            if (!PlayerMoves.isDead)
                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = collision.gameObject.GetComponent<PlayerMoves>().NonMuoversi();
            else PlayerMoves.isDead = false;
    }
}}
