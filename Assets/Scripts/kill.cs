using System.Threading;
using UnityEngine;
using System.Collections;

public class kill : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //controlla se la collisione e' col player, lo trasporta al checkpoint attivo, abbassa le vite e avvia l'animazione di morte
            //se non è morto, attiva e NuonMuoversi, altrimenti lo considera non morto (andando a triggerare il se)
            collision.transform.position = Checkpoint.GetActiveCheckpoint();
            GameManager.Vite--;
            SpriteManager.Instance.ActiveSprite.GetComponent<Animator>().SetTrigger("LostLife");
            if (!PlayerMoves.isDead)
                collision.gameObject.GetComponent<Rigidbody2D>().linearVelocity = collision.gameObject.GetComponent<PlayerMoves>().NonMuoversi();
            else PlayerMoves.isDead = false;
        }
    }
}
