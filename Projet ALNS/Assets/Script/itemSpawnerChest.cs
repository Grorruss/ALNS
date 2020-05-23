using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawnerChest : MonoBehaviour
{
    [SerializeField] private ObjectInfo[] objects;  // fill in editor

    GameObject GetRandomObject(ObjectInfo[] objects)
    {
        float chance = Random.Range(1, 100); // random (0 to 99) %
        GameObject toReturn = null;

        foreach (ObjectInfo obj in objects)
        {
            // Check if random is in chance
            if (chance < obj.chance)
            {
                toReturn = null;
                toReturn = obj.obj; // returns object
                chance -= (int)obj.chance;
            }
            // Fix chance for next item
        }
        return toReturn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            Destroy(this.GetComponent<BoxCollider2D>());
            Debug.Log("Instantiate");
            //Destroy(gameObject);
            Vector3 chestTop = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            GameObject objet = Instantiate(GetRandomObject(objects), chestTop, Quaternion.identity); // single use
        }
    }
}

[System.Serializable]
public struct ObjectInfo
{ // structure for object information
    public GameObject obj; // Prefab
    public short chance; // (0 to 99) %
}

