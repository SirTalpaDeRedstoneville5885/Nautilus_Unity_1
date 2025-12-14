using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            //    Debug.Log("Player Toccato! Ucciso oggetto");
            collision.transform.GetComponent<PlayerMoves>().Jump();
            GameManager.Monete += 10;
            Destroy(Enemy);
        }
    }
}
