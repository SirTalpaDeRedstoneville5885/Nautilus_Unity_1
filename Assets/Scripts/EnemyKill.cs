using UnityEngine;

public class EnemyKill : MonoBehaviour
{
    [SerializeField] GameObject Enemy, Drop;
    bool Done = false;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            Vector3 dropPos = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
            // prende l'oggetto che ha triggerato la funzione, contolla se ha il tag player, lo fa saltare e aumenta le monete
            // poi distrugge il parent del nemico, per cancellare tutto, pos hitbox e nemico inclusee infine se e' assegnato un un drop, lo instanzia
            collision.transform.GetComponent<PlayerMoves>().Jump();
            GameManager.Monete += 10;
            Destroy(Enemy);
            if (Drop != null && !Done) { Instantiate(Drop, dropPos, Quaternion.Euler(0, 0, 0)); Done = true; }
        }
    }
}
