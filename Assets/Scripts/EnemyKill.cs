using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] bool HasDrop = false;
    [SerializeField] GameObject Drop;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            collision.transform.GetComponent<PlayerMoves>().Jump();
            GameManager.Monete += 10;
            if (HasDrop) Instantiate(Drop, new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z), transform.rotation);
            Destroy(Enemy);
        }
    }
}
