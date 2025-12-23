using System.Collections;
using UnityEngine;

public class SlimePowerUp : MonoBehaviour
{
    //[SerializeField] PhysicsMaterial2D NoFriction;
    void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.AudioList[6].volume = 5f;
        AudioManager.Instance.AudioList[6].Play();
        if (collision.gameObject.CompareTag(("Player")) && !PlayerMoves.isSlimed)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = null;
            SpriteManager.Instance.ActiveSprite.GetComponent<SpriteRenderer>().material.color = Color.green;
            PlayerMoves.isSlimed = true;
        }
        else GameManager.Monete += 10;
        Destroy(gameObject);
    }
}
