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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
            Destroy(gameObject);
        }
    }
}
