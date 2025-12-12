using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    int secondi=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    IEnumerator TimerGioco()
    {
        Debug.Log("Secondi Trascorsi: "+ secondi);
        yield return new WaitForSeconds(1f);
        secondi++;
        StartCoroutine(TimerGioco());
    }

}
