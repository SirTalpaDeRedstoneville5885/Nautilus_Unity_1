using UnityEngine;

public class WingsPowerUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMoves.JumpMax++;
        Destroy(gameObject);
    }
}
