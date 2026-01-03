using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Ricomincia()
    {
        //ricomincia il gioco
        GameManager.LV = 1;
        SceneManager.LoadScene("Livello1");
    }
    void Start()
    {
        //rende usabile il cursore e sposta lo sprite manager
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SpriteManager.Instance.HideActiveSprite();
    }
}

