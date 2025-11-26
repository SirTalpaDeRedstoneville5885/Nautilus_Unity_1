using UnityEngine;

public class Collect : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            //aggiornare contatore, la moneta va distrutta
            GameManager.Monete++;
            //Debug.Log("Ho raccolto la moneta!");
            Destroy(gameObject);
        }
    }
}
