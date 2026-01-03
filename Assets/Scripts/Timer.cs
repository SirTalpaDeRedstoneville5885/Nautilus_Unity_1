using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    int secondi = 0, minuti = 0, ore = 0;
    [SerializeField] TextMeshProUGUI TimerTesto;
    void Start()
    {
        StartCoroutine(TimerGioco());
    }
    IEnumerator TimerGioco()
    {
        //setta il testo uguale al timer, controlla quanti secondi ci sono e cambia minuti e ore, poi conta i secondi, e si chiama in modo ricorsivo 
        TimerTesto.text = ore.ToString() + ':' + minuti.ToString() + ':' + secondi.ToString();
        if (secondi == 60)
        {
            secondi = 0;
            minuti++;
        }
        if (minuti == 60)
        {
            minuti = 0;
            ore++;
        }
        yield return new WaitForSeconds(1f);
        secondi++;
        StartCoroutine(TimerGioco());
    }
}
