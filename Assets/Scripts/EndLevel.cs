using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    [SerializeField] AudioSource EndSound;
    void Start()
    {
        EndSound.Play();
        SpriteManager.Instance.HideActiveSprite();
    }

    public void BackMenu()
    {
        EndSound.Stop();
        SceneManager.LoadScene("Menu");
    }
    public void CloseGame()
    {
        Application.Quit();
    }
}
