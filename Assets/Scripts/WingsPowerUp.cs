using UnityEngine;

public class WingsPowerUp : MonoBehaviour
{
    bool Done = false;
    [SerializeField] LayerMask groundMask;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Done && collision.gameObject.CompareTag("Player")) // se non si è ancora attivata questa instanza,
        // fa partire l'audio, setta un bool in modo che succeda una sola volta, e aumenta i possibili salti da fare, e infine distrugge l'oggetto
        {
            AudioManager.Instance.AudioList[7].Play();
            Done = true;
            PlayerMoves.JumpToDo++;
            Destroy(gameObject);
        }
    }
    void LateUpdate()
    {
        if (Physics2D.OverlapCircle(transform.position, .6f, groundMask))
        /* se tocca terra smette di cadere*/
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
