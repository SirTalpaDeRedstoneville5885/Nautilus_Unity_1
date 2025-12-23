using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;
    [SerializeField] public GameObject[] PlayerBody;
    [SerializeField] public GameObject AliBody;
    public GameObject ActiveSprite;
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
    void OnEnable() // in modo che sia dopo l'awake e prima dello start
    {
        if (ActiveSprite == null) ActiveSprite = PlayerBody[0];
        ActiveSprite.transform.localScale = new Vector3(1f, 1f, 1f);
        foreach (GameObject t in PlayerBody)
        {
            t.SetActive(false);
        }
    }
}
