using UnityEngine;
using TMPro;
using NUnit.Framework.Internal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int Monete;
    [SerializeField] TextMeshProUGUI TestoMonete;
    [SerializeField] TextMeshProUGUI TestoVite;

    public static int Vite;

    void Update()
    {
        TestoMonete.text = Monete.ToString();
        TestoVite.text = Vite.ToString();

        if (Vite <= 0)
        {
            //Debug.Log("Ho motto :(");
            SceneManager.LoadScene("GameOver");
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
