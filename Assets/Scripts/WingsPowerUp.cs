using UnityEngine;

public class WingsPowerUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool Done = false;
    [SerializeField] LayerMask groundMask;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Done && (collision.gameObject.CompareTag(("Player"))))
        {
            Done = true;
            PlayerMoves.JumpMax++;
            Destroy(gameObject);
        }
        if (Physics2D.OverlapCircle(transform.position, .6f, groundMask))
        {
            GetComponent<Rigidbody2D>().simulated = false;
        }
    }
    void LateUpdate()
    {
        if (Physics2D.OverlapCircle(transform.position, .6f, groundMask))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
