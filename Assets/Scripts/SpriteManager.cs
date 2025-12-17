using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;
    [SerializeField] public GameObject[] PlayerBody;
    public int SpriteIndex = -1;
    public static GameObject ActiveSprite;
    public AudioSource FootstepsSound;
    void Start()
    {
        ActiveSprite = PlayerBody[SpriteIndex];
        ActiveSprite.transform.localScale = new Vector3(1f, 1f, 1f);
        foreach (GameObject t in PlayerBody)
        {
            t.SetActive(false);
        }
        ActiveSprite.SetActive(true);
        if (SpriteIndex == 0) FootstepsSound = AudioManager.Instance.AudioList[3].GetComponent<AudioSource>();
        if (SpriteIndex == 1) FootstepsSound = AudioManager.Instance.AudioList[6].GetComponent<AudioSource>();
    }
    void Awake()
    {
        if (SpriteIndex < 0) SpriteIndex = 0;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Update()
    {
        ActiveSprite.SetActive(true);
    }
}
