using System.Collections;
using UnityEngine;

public class SlimePowerUp : MonoBehaviour
{
    // dopo essere stato toccato da un player, fa partire un suono
    // se viene toccato da un player non slimed, ne rende vuoto il materiale, cambia il colore dello sprite e setta un bool, in modo che sia slimed
    // se invece viene toccato da un player gia' slimed, da dieci monete. 
    // poi distrugge l'oggetto
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            float f;
            f = AudioManager.Instance.AudioList[6].volume;
            AudioManager.Instance.AudioList[6].volume = 5f;
            AudioManager.Instance.AudioList[6].Play();
            AudioManager.Instance.AudioList[6].volume = f;
            if (!PlayerMoves.isSlimed)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = null;
                SpriteManager.Instance.ActiveSprite.GetComponent<SpriteRenderer>().material.color = Color.green;
                PlayerMoves.isSlimed = true;
            }
            else GameManager.Monete += 10;
            Destroy(gameObject);
        }
    }
}
