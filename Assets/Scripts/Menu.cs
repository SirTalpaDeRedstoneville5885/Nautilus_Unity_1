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
    void OnEnable()
    {
        // ogni volta che viene caricato, in base all'aver o meno sbloccato il personaggio ne disattiva la copertura
        if (GameManager.SlimeSbloccato)
            CopChar[0].SetActive(false);
    }
    void Start()
    {
        //disattiva le musiche, se serve avvia un debug per lo slime, sblocca e rende visibile il cursore,
        //disattiva il testo di ogni copertura, avvia la musica del menu e sposta lo spritemanager per non vederlo in scena
        foreach (AudioSource t in AudioManager.Instance.AudioList) t.Stop();
        GameManager.SlimeSbloccato = SlimeDebug;
        Pause.AchievedSlime = SlimeDebug;
        DisattivaPanelTranne(MenuPanel);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        foreach (GameObject t in CopChar)
        {
            t.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
        }
        AudioManager.Instance.AudioList[8].Play();
        SpriteManager.Instance.HideActiveSprite();

    }
    void DisattivaPanelTranne(GameObject StayActive)
    // disattiva tutti i vari panel tranne uno
    {
        MenuPanel.SetActive(false);
        CharacterPanel.SetActive(false);
        CreditPanel.SetActive(false);
        StayActive.SetActive(true);
    }
    public void Play()
    {
        //setta l'indicatore del livello attuale a 1, e carica il livello attuale 
        GameManager.LV = 1;
        SceneManager.LoadScene("Livello" + GameManager.LV.ToString());
    }
    public void LoadChars()
    {
        //apre il pannell
        DisattivaPanelTranne(CharacterPanel);
    }
    public void LockedChar(int SL)
    {
        //attiva il testo e un suono del pulsante appena premuto
        CopChar[SL].GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(true);
        AudioManager.Instance.AudioList[5].Play();
    }
    public void CloseChars()
    {
        //attiva il panel principale e disattiva i testi in tutte le coperture
        DisattivaPanelTranne(MenuPanel);
        foreach (GameObject t in CopChar)
        {
            t.GetComponentInChildren<TextMeshProUGUI>(true).gameObject.SetActive(false);
        }
    }
    public void SelectChar(int SL)
    {
        //assegna all'active sprite la propria identita' e  sposta un indicatore sul pulsante
        SpriteManager.Instance.ActiveSprite = SpriteManager.Instance.PlayerBody[SL];
        Frame.transform.position = BottoniChar[SL].GetComponent<Transform>().position;
    }
    public void Credits()
    {
        DisattivaPanelTranne(CreditPanel);
    }
    public void CloseCredits()
    {
        DisattivaPanelTranne(MenuPanel);
    }
    public void CloseGame()
    //chiude il gioco
    {
        Application.Quit();
    }
}
