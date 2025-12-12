using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject Camera, Player;
    void Update()
    {
        Camera.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 2.75f, -10);
    }
}
//https://www.smbgames.be/super-mario-brothers.php
