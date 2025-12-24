using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false, AchievementSlime = false;
    [SerializeField] GameObject pausePanel, OptionPanel, AchievementButton;
    float tm = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausePanel.SetActive(false);
        OptionPanel.SetActive(false);
        AchievementButton.SetActive(false);
        if (GameManager.SlimeSbloccato) AchievementButton.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
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
        if (GameManager.SlimeSbloccato && !AchievementSlime)
        {
            AchievementButton.SetActive(true);
            tm += Time.deltaTime;
            if (tm > 8f)
            {
                AchievementButton.SetActive(false);
                AchievementSlime = true;
            }
        }
    }
    public void Resume()
    {
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
        PlayerMoves.JumpMax = 1;
        SpriteManager.Instance.ActiveSprite.GetComponent<SpriteRenderer>().material.color = Color.white;
        if (SpriteManager.Instance.ActiveSprite != SpriteManager.Instance.PlayerBody[1]) PlayerMoves.isSlimed = false;
        GameIsPaused = false;
        Resume();
        SceneManager.LoadScene(NomeScena);
    }
    public void LoadMenu()
    {
        GameIsPaused = false;
        Resume();
        SceneManager.LoadScene("Menu");
    }
}
