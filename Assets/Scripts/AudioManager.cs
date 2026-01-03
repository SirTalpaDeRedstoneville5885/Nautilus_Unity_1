using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource[] AudioList;
    void Awake()
    {
        //crea un singleton in modo da essere accessibile senza riferimenti diretti, ma solo essendo in scena
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
