using System.Security.Cryptography;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject particles;
    public bool Activated = false;
    public static GameObject[] CheckPointList;


    public static Vector3 GetActiveCheckpoint()
    {
        // restituisce la posizione dell'ultimo checkpoint attivato o 0,0,0
        Vector3 result = new Vector3(0, 0, 0);
        if (CheckPointList != null)
        {
            foreach (GameObject cp in CheckPointList)
            {
                if (cp.GetComponent<Checkpoint>().Activated)
                {
                    result = cp.transform.position;
                    break;
                }
            }
        }
        return result;
    }
    private void ActivateCheckPoint()
    {
        //disattiva tutti i checkpoint tranne quello appena toccato, e crea delle particelle temporanee
        foreach (GameObject cp in CheckPointList)
        {
            cp.GetComponent<Checkpoint>().Activated = false;
        }
        this.Activated = true;
        GameObject particlesClone = Instantiate(particles, transform.position, transform.rotation);
        Destroy(particlesClone, 2f);
    }
    void Start()
    {
        // crea un'array di tutti gli oggetti col tag Checkpoint
        CheckPointList = GameObject.FindGameObjectsWithTag("Checkpoint");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !this.Activated)
        {
            ActivateCheckPoint();
        }
    }
}
