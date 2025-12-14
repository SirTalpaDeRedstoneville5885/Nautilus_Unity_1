using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pausePanel, OptionPanel, AchievementButton;
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
        GameIsPaused = false;
        Resume();
        SceneManager.LoadScene(NomeScena);
    }
    public void Achievement()
    {
        GameIsPaused = false;
        Resume();
        SceneManager.LoadScene("Menu");
        if (GameManager.SlimeSbloccato) Destroy(this);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pausePanel.SetActive(false);
        OptionPanel.SetActive(false);
        AchievementButton.SetActive(false);
    }

    // Update is called once per frame
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
        AchievementButton.SetActive(GameManager.SlimeSbloccato);
    }
}
