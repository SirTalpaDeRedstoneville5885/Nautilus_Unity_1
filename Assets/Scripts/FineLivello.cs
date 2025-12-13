using System.Security.Cryptography;
using UnityEngine;

public class FineLivello : MonoBehaviour
{
    [SerializeField] GameObject particles;
    public bool Activated = false;
    public static GameObject[] FlagList;
    private void ActivateFlag()
    {
        this.Activated = true;
        GameObject particlesClone = Instantiate(particles, transform.position, transform.rotation);
        GameManager.FineLivello = true;
        Destroy(particlesClone, 2f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FlagList = GameObject.FindGameObjectsWithTag("Flag");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !this.Activated)
        {
            ActivateFlag();
        }
    }
}
