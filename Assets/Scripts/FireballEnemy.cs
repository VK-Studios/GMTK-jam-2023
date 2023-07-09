using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireballEnemy : MonoBehaviour
{

    private Rigidbody2D rb;
    private int life = 60;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
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
                collision.gameObject.GetComponent<IDamageable>().dealDamage(5);
            } 


            Destroy(gameObject);
        }
	}
}
