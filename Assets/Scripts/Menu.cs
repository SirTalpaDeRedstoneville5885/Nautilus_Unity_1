using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public bool SlimeDebug;
    [SerializeField] GameObject MenuPanel, CharacterPanel, CreditPanel, Frame;
    [SerializeField] GameObject[] BottoniChar, CopChar;
    void DisattivaTranne(GameObject Ciccio)
    {
        MenuPanel.SetActive(false);
        CharacterPanel.SetActive(false);
        CreditPanel.SetActive(false);
        Ciccio.SetActive(true);
    }
    public void Play()
    {
        GameManager.LV = 1;
        SceneManager.LoadScene("Livello1");
    }
    public void LoadChars()
    {
        DisattivaTranne(CharacterPanel);
    }
    public void LockedChar(int SL)
    {
        CopChar[SL].GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(true);
        AudioManager.Instance.AudioList[5].Play();
    }
    public void CloseChars()
    {
        DisattivaTranne(MenuPanel);
        foreach (GameObject t in CopChar)
        {
            t.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(false);
        }
    }
    void OnEnable()
    {
        if (GameManager.SlimeSbloccato)
            CopChar[0].SetActive(false);
    }
    void Start()
    {
        GameManager.SlimeSbloccato = SlimeDebug;
        Pause.AchievementSlime = SlimeDebug;
        DisattivaTranne(MenuPanel);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        CopChar[0].GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
        if (AudioManager.Instance.AudioList[0].isPlaying) AudioManager.Instance.AudioList[0].Stop();
    }

    public void SelectChar(int SL)
    {
        SpriteManager.Instance.ActiveSprite = SpriteManager.Instance.PlayerBody[SL];
        Frame.transform.position = BottoniChar[SL].GetComponent<Transform>().position;
    }
    public void Credits()
    {
        DisattivaTranne(CreditPanel);
    }
    public void CloseCredits()
    {
        DisattivaTranne(MenuPanel);
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
