using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag(("Player"))){
            collision.transform.GetComponent<PlayerMoves>().Jump();
            Destroy(Enemy);
        }
    }
}
