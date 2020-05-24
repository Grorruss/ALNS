using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    public GameObject teleportDoor;

    void OnDestroy()
    {
        Room room = CameraController.instance.currRoom;
        GameObject teleport = Instantiate(teleportDoor, room.GetRoomCentre(), transform.rotation) as GameObject;
    }
}
