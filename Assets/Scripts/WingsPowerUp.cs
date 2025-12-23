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
            AudioManager.Instance.AudioList[7].Play();
            Done = true;
            PlayerMoves.JumpMax++;
            Destroy(gameObject);
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
