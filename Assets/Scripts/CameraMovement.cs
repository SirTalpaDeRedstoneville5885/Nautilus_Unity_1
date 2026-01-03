using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera MainCam;
    [SerializeField] GameObject Player;
    void LateUpdate()
    {
        // setta la posizione della camera secondo quella del player e cambia il fov in base alle monete
        MainCam.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2.75f, -10);
        switch (GameManager.Monete)
        {
            case >= 81:
                {
                    MainCam.fieldOfView = 70f;
                    break;
                }
            case >= 54:
                {
                    MainCam.fieldOfView = 65f;
                    break;
                }
            case >= 27:
                {
                    MainCam.fieldOfView = 55f;
                    break;
                }
        }
    }
}
//https://www.smbgames.be/super-mario-brothers.php
