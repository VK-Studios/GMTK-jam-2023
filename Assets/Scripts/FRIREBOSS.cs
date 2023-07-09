using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class FIRIREBOSS : MonoBehaviour
{

    private Rigidbody2D rb;
    private int life = 60;
    public GameObject player;
    public int dam;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        dam = player.GetComponent<pointAtk>().damageOfFire;

    }

    // Update is called once per frame
    void Update()
    {

    }

	private void FixedUpdate() {
        life -= 1;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
        rb.velocity = new Vector2(transform.up.y * 10, -transform.up.x * 10);
	}


	private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.name != "Interaction Range" && collision.gameObject.tag != "cast" && !collision.isTrigger) {
            
            //add interface for damamge here, IDamageable
           if(collision.gameObject.GetComponent<IDamageable>() != null)
            {
                collision.gameObject.GetComponent<IDamageable>().dealDamage(dam);
            } 


            Destroy(gameObject);
        }
	}
}
