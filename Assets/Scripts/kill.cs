using UnityEngine;

public class kill : MonoBehaviour
{
    [SerializeField] Vector2 RespawnPoint;
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag(("Player"))){
            Debug.Log("Player Toccato!");
            collision.transform.position = RespawnPoint;
        }
    }
}
