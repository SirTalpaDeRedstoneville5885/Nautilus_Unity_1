using UnityEngine;

public class SlimePowerUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag(("Player"))) && !PlayerMoves.isSlimed)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = null;
            // SpriteRenderer sr = SpriteManager.ActiveSprite.GetComponent<SpriteRenderer>().material.color;
            // sr.material = new Material(sr.material);
            // sr.material.color = Color.green;
            SpriteManager.ActiveSprite.GetComponent<SpriteRenderer>().material.color = Color.green;
            PlayerMoves.isSlimed = true;
        }
        else GameManager.Monete *= 10;
        Destroy(gameObject);
    }
}
