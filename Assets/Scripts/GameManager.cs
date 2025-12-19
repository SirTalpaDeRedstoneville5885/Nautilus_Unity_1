using UnityEngine;
using TMPro;
using NUnit.Framework.Internal;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static int Monete, LV = 1;
    public static bool FineLivello, SlimeSbloccato = false;
    [SerializeField] TextMeshProUGUI TestoMonete, TestoVite, TestoSalti;

    public static int Vite;

    private string CalcolaSalti()
    {
        int r = PlayerMoves.JumpMax + 1;
        return ("Numero salti: " + r.ToString());
    }

    void Update()
    {
        TestoMonete.text = Monete.ToString();
        TestoVite.text = Vite.ToString();
        TestoSalti.text = CalcolaSalti();
        if (Vite <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        if (Monete >= 100 && Monete != 0 && FineLivello)
        {
            LV++;
            Monete = 0;
            SceneManager.LoadScene("Livello" + LV);
        }
        if (PlayerMoves.isSlimed && FineLivello) SlimeSbloccato = true;
    }

    void Start()
    {
        // Bloccare e nascondere il mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Monete = 0;
        Vite = 5;
        FineLivello = false;
    }
}
