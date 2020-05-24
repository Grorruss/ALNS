
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawnerChest : MonoBehaviour
{
    public GameObject[] items;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Destroy(this.GetComponent<BoxCollider2D>());
            Debug.Log("Instantiate");
            Destroy(gameObject);
            Vector3 chestTop = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            GameObject newItem = items[Random.Range(0, items.Length)];
            Instantiate(newItem, chestTop, Quaternion.identity); // single use

        }
    }
}

