using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject MenuPanel, CharacterPanel, Frame, Cop1;
    [SerializeField] GameObject[] bottoni;
    [SerializeField] AudioSource LockedSound;
    public void Play()
    {
        GameManager.LV = 0;
        SceneManager.LoadScene("Livello1");
    }
    public void LoadChars()
    {
        MenuPanel.SetActive(false);
        CharacterPanel.SetActive(true);
    }
    public void LockedChar()
    {
        Cop1.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(true);
        LockedSound.Play();
    }
    public void CloseChars()
    {
        MenuPanel.SetActive(true);
        CharacterPanel.SetActive(false);
        Cop1.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(false);
    }
    void Start()
    {
        CharacterPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Cop1.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
        if (GameManager.SlimeSbloccato) Destroy(Cop1);
    }

    public void SelectChar()
    {
        Frame.transform.position = this.gameObject.transform.position;
        //return 0;
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
