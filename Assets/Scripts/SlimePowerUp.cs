using UnityEngine;

public class SlimePowerUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] SpriteRenderer PlayerBody;
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMoves.Instance.rb2d.sharedMaterial = null;
        SpriteRenderer sr = PlayerBody;
        sr.material = new Material(sr.material);
        sr.material.color = Color.green;
        PlayerMoves.isSlimed = true;
        Destroy(gameObject);
    }
}
