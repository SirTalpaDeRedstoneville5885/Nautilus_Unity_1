using System.Security.Cryptography;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
[SerializeField] GameObject particles;
public bool Activated = false;
public static GameObject[] CheckPointList;


public static Vector3 GetActiveCheckpoint()
    {
        Vector3 result = new Vector3(0,0,0);
        if (CheckPointList != null)
        {
            foreach(GameObject cp in CheckPointList)
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
    foreach(GameObject cp in CheckPointList)
    {
        cp.GetComponent<Checkpoint>().Activated=false;
    }
    this.Activated = true;
    GameObject particlesClone = Instantiate(particles,transform.position,transform.rotation);
    Destroy(particlesClone,2f);
}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
