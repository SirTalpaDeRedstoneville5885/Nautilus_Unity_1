using System.Security.Cryptography;
using UnityEngine;

public class FineLivello : MonoBehaviour
{
    [SerializeField] GameObject particles;
    public bool Activated = false;
    public static GameObject[] FlagList;
    private void ActivateFlag()
    {
        // cambia il bool in modo che questo sia attivo, e da indizi visivi sul fatto cyhe sia attivo, quindi comunica che il giocatore ha finito il livello
        this.Activated = true;
        GameObject particlesClone = Instantiate(particles, transform.position, transform.rotation);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        GameManager.FineLivello = true;
        Destroy(particlesClone, 2f);
    }
    void Start()
    {
        // crea un'array di oggetti col tag Flag, dei fine livello
        FlagList = GameObject.FindGameObjectsWithTag("Flag");
    }
    void LateUpdate()
    {
        // controlla se il giocatore ha raccolto abbastanza monete e cambia il colore per indicare che si e' attivato il gameobject
        if (GameManager.Monete > 99)
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0f, 1f);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !this.Activated && GameManager.Monete > 99)
        // se non e' attivo, lo tocca un player con almeno 100 monete, attiva
        {
            ActivateFlag();
        }
    }
}
