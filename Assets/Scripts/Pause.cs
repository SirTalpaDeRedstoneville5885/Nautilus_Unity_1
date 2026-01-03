using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false, AchievedSlime = false;
    [SerializeField] GameObject pausePanel, OptionPanel, AchievementButton;
    float tm = 0f;
    void Start()
    {
        // disattiva tutti i pannelli, e se si ha già sbloccato lo slime anche il pulsante dello slime
        pausePanel.SetActive(false);
        OptionPanel.SetActive(false);
        AchievementButton.SetActive(false);
        if (GameManager.SlimeSbloccato) AchievementButton.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        // fa un check se viene premuto il pulsante esc per negare il bool di pausa, e se e' attivo dopo essere stato premuto stoppa la musica del livello e attiva il panel della pausa, disattivando quello delle opzioni se ancora era attivo 
        {
            GameIsPaused = !GameIsPaused;
            if (GameIsPaused)
            {
                AudioManager.Instance.AudioList[0].Stop();
                pausePanel.SetActive(true);
                OptionPanel.SetActive(false);
            }
        }
        if (!GameIsPaused)
        // mette o meno in pausa
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pausePanel.SetActive(GameIsPaused);
            OptionPanel.SetActive(GameIsPaused);
            Resume();
        }
        else
        {
            Paused();
        }
        if (GameManager.SlimeSbloccato && !AchievedSlime)
        // avvia un timer che fa vedere per un certo tempo il bottone di achievement slime, e poi lo disattiva
        {
            AchievementButton.SetActive(true);
            tm += Time.deltaTime;
            if (tm > 8f)
            {
                AchievementButton.SetActive(false);
                AchievedSlime = true;
            }
        }
    }
    public void Resume()
    {
        //blocca il cursore, cambia il bool di pausa, disattiva i pannelli di pausa cambia il timescale e avvia la musica della partita se non lo era gia'
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        if (!AudioManager.Instance.AudioList[0].isPlaying)
        {
            AudioManager.Instance.AudioList[0].Play();
            AudioManager.Instance.AudioList[1].Stop();
        }
    }
    void Paused()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPaused = true;
        Time.timeScale = 0f;
        if (!AudioManager.Instance.AudioList[1].isPlaying)
        {
            AudioManager.Instance.AudioList[1].Play();
            AudioManager.Instance.AudioList[0].Stop();
        }
    }
    public void LoadaScene(string NomeScena)
    {
        //reimposta varie cose allo stato di inizio del livello, e poi avvia resume e carica un livello
        PlayerMoves.JumpToDo = 0;
        SpriteManager.Instance.ActiveSprite.GetComponent<SpriteRenderer>().material.color = Color.white;
        if (SpriteManager.Instance.ActiveSprite != SpriteManager.Instance.PlayerBody[1]) PlayerMoves.isSlimed = false;
        Resume();
        SceneManager.LoadScene(NomeScena);
    }
    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene("Menu");
    }
}
