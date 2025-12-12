using UnityEngine;
using TMPro;
using NUnit.Framework.Internal;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static int Monete;
    public static int LV = 1;
    [SerializeField] TextMeshProUGUI TestoMonete;
    [SerializeField] TextMeshProUGUI TestoVite;

    public static int Vite;

    void Update()
    {
        TestoMonete.text = Monete.ToString();
        TestoVite.text = Vite.ToString();

        if (Vite <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (Monete >= 100 && Monete != 0)
        {
            LV++;
            Monete = 0;
            SceneManager.LoadScene("Livello" + LV);
        }
    }

    void Start()
    {
        // Bloccare e nascondere il mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Monete = 0;
        Vite = 5;
    }
}
