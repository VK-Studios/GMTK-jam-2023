using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orb : MonoBehaviour
{
    public respawnw respawn;

    private void Start()
    {
        respawn = GameObject.FindFirstObjectByType<respawnw>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            if(collision.gameObject.name != "Interaction Range")
            {
                Destroy(gameObject);
                respawn.addOrb();
            }

        }
    }
}