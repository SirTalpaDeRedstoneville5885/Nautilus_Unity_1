using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance;
    [SerializeField] public GameObject[] PlayerBody;
    [SerializeField] public GameObject AliBody;
    public GameObject ActiveSprite;
    void Awake()
    {
        // crea un singleton o distrugge le copie già in scena
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
        //imposta un'active sprite qualora non sia impostato e disattiva tutti gli sprite aggiuntivi
        if (ActiveSprite == null) ActiveSprite = PlayerBody[0];
        foreach (GameObject t in PlayerBody)
        {
            t.SetActive(false);
        }
    }
    public void HideActiveSprite()
    {
        //sposta lo sprite manager per evitare che dia fastidio dentro la camera
        transform.position = new Vector3(900f, 200f, 0f);
    }
}
