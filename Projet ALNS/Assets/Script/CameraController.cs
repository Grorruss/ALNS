using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Room currRoom;
    public float moveSpeedOnChangeRoom;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if(currRoom==null){ return; }

        Vector3 targetPos = GetCameraTargetPostion();
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedOnChangeRoom);
    }

    Vector3 GetCameraTargetPostion()
    {
        if(currRoom ==null){ return Vector3.zero; }
        Vector3 targetPos = currRoom.GetRoomCentre();
        targetPos.z = transform.position.z;
        return targetPos;
    }

    public bool IsSwitchingScene(){
        return transform.position.Equals(GetCameraTargetPostion()) == false;
    }
}
