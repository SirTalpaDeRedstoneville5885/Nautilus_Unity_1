using UnityEngine;
using TMPro;
using NUnit.Framework.Internal;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static int Monete, LV = 1, Vite;
    public static bool FineLivello, SlimeSbloccato = false;
    [SerializeField] TextMeshProUGUI TestoMonete, TestoVite;

    void Update()
    {
        // assegno ai testi i valori degli interi e gestisce achievement, vitee  fine livelli
        TestoMonete.text = Monete.ToString();
        TestoVite.text = Vite.ToString();
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
        // Blocca e nasconde il mouse, poi imposta delle variabili allo stato iniziale per ogni livello
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Monete = 0;
        Vite = 5;
        FineLivello = false;
    }
}
