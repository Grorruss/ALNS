using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleportplayer : MonoBehaviour
{

    public string whereToTeleport = "Forest";

   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
         


                SceneManager.LoadScene(whereToTeleport + "Main");
            }
        
        }
    }

