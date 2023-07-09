using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{

    public bool spawnOne = true;
    public int spawnNum = 1;
    public int spawned = 0;
    public float spawnDelay = 2f;
	private float spawnCoolCounter;
    public GameObject player;

	public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnOne) {
            spawn();
		}
        
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    
        if(Math.Abs(player.transform.position.x - transform.position.x) < 30)        {
            if (!spawnOne && spawned <= spawnNum - 1)
            {
                if (spawnCoolCounter <= 0)
                {
                    spawn();
                    spawned++;
                    spawnCoolCounter = spawnDelay;
                }


                if (spawnCoolCounter > 0)
                {
                    spawnCoolCounter -= Time.deltaTime;
                }


            }
        }
        
    }

    public void spawn() {
		Instantiate(enemy, transform.position, transform.rotation);
	}
}
