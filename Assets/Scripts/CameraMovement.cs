using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera MainCam;
    [SerializeField] GameObject Player;
    bool cameraSized1 = false, cameraSized2 = false, cameraSized3 = false;
    void Update()
    {
        MainCam.GetComponent<Transform>().position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2.75f, -10);
        if (GameManager.Monete >= 27 && !cameraSized1)
        {
            MainCam.fieldOfView = 55f;
            cameraSized1 = true;
            cameraSized2 = false;
            cameraSized3 = false;
        }
        if (GameManager.Monete >= 54 && !cameraSized2)
        {
            MainCam.fieldOfView = 65f;

            cameraSized1 = false;
            cameraSized2 = true;
            cameraSized3 = false;
        }
        if (GameManager.Monete >= 81 && !cameraSized3)
        {
            MainCam.fieldOfView = 70f;
            cameraSized1 = false;
            cameraSized2 = false;
            cameraSized3 = true;
        }
    }
}
//https://www.smbgames.be/super-mario-brothers.php
