using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class fightScene : MonoBehaviour
{

    public DialogueBox db;
    private bool ro = false;
    public GameObject sp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
	{
        if(db.complete && !ro) {

            Vector3 heck = new Vector3(transform.position.x+20, transform.position.y+10, transform.position.z);


			GameObject spaa = Instantiate(sp, heck, transform.rotation);

            spaa.GetComponent<spawnEnemy>().spawnOne = false;

            spaa.GetComponent<spawnEnemy>().spawnNum = 3;

            spaa.GetComponent<spawnEnemy>().player = GameObject.FindGameObjectWithTag("Player");

			ro = true;
        }
    }
}
