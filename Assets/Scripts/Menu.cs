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
    [SerializeField] AudioSource LockedSound;
    private void DisattivaTranne(GameObject Ciccio)
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
    public void LockedChar()
    {
        CopChar[0].GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(true);
        LockedSound.Play();
    }
    public void CloseChars()
    {
        DisattivaTranne(MenuPanel);
        CopChar[0].GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(false);
    }
    void Start()
    {
        GameManager.SlimeSbloccato = SlimeDebug;
        DisattivaTranne(MenuPanel);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        CopChar[0].GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
        if (GameManager.SlimeSbloccato) Destroy(CopChar[0]);
    }

    public void SelectChar(int SL)
    {
        SpriteManager.Instance.SpriteIndex = SL;
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
