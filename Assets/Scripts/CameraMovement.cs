using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera MainCam;
    [SerializeField] GameObject Player;
    bool cameraSized1 = false, cameraSized2 = false, cameraSized3 = false;
    void LateUpdate()
    {
        MainCam.GetComponent<Transform>().position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2.75f, -10);
        if (GameManager.Monete >= 27 && !cameraSized1)
            cameraSized1 = setFov(55f);
        if (GameManager.Monete >= 54 && !cameraSized2)
            cameraSized2 = setFov(65f);
        if (GameManager.Monete >= 81 && !cameraSized3)
            cameraSized3 = setFov(70f);
    }
    bool setFov(float fov)
    {
        MainCam.fieldOfView = fov;
        cameraSized1 = false;
        cameraSized2 = false;
        cameraSized3 = false;
        return true;
    }
}
//https://www.smbgames.be/super-mario-brothers.php
