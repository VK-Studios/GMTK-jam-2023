using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemy : MonoBehaviour
{

    public bool spawnOne = true;
    public int spawnNum = 1;
    private int spawned = 0;
    public float spawnDelay = 2f;
	private float spawnCoolCounter;

	public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnOne) {
            spawn();
		}
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnOne) {
            if (spawnCoolCounter <= 0) {
				spawn();
                spawnCoolCounter = spawnDelay;
			}


            if (spawnCoolCounter > 0) {
				spawnCoolCounter -= Time.deltaTime;
			}
            

        }
    }

    public void spawn() {
		Instantiate(enemy, transform.position, transform.rotation);
	}
}
