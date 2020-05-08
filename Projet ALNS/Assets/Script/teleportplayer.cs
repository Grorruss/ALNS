using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleportplayer : MonoBehaviour
{

    List<string> listLevels = new List<string>(new string[] { "Cellar", "Basement" });
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            /*Debug.Log(listLevels.Count);
            Debug.Log(RoomController.instance.currentWorldName);
            Debug.Log(listLevels.IndexOf(RoomController.instance.currentWorldName) + 1);*/

            if (listLevels.Count > listLevels.IndexOf(RoomController.instance.currentWorldName) + 1)
            {

                int indexLevel = listLevels.IndexOf(RoomController.instance.currentWorldName) + 1;
                Debug.Log(indexLevel);
                Debug.Log(listLevels[indexLevel] + "Main");

                RoomController.instance.currentWorldName = listLevels[indexLevel];
                SceneManager.LoadScene(listLevels[indexLevel] + "Main");
            }
            else
            {
                //Load Last map or screen title
            }
        }
    }
}
