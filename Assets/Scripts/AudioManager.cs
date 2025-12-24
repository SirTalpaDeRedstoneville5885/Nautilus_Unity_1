using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] AudioList;
    public static AudioManager Instance;

    void Awake()
    {
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
