using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public int x;
    public int y;

    private bool updatedDoors = false;

    public Door topDoor;
    public Door leftDoor;
    public Door downDoor;
    public Door rightDoor;

    public List<Door> doors = new List<Door>();

    public Room(int x, int y){
        this.x = x;
        this.y = y;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("Wrong Scene");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds){
            doors.Add(d);
            switch(d.doorType){
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.bottom:
                    downDoor = d;
                    break;
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }

    void Update()
    {
        if(name.Contains("End") && !updatedDoors){
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.top:
                    if (GetTop() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.right:
                    if (GetRight() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }

    public Room GetTop(){
        if(RoomController.instance.DoesRoomExist(x,y + 1)){
            return RoomController.instance.FindRoom(x, y + 1);
        }
        return null;
    }
    public Room GetLeft(){
        if (RoomController.instance.DoesRoomExist(x - 1, y))
        {
            return RoomController.instance.FindRoom(x - 1, y);
        }
        return null;
    } 
    public Room GetBottom(){
        if (RoomController.instance.DoesRoomExist(x, y - 1))
        {
            return RoomController.instance.FindRoom(x, y - 1);
        }
        return null;
    }
    public Room GetRight(){
        if (RoomController.instance.DoesRoomExist(x + 1, y))
        {
            return RoomController.instance.FindRoom(x + 1, y);
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(x * width, y * height);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"){
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
