using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fireball : MonoBehaviour
{

    private Rigidbody2D rb;
    private int life = 60;
    public int fireDamage;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setDamage(int damage)
    {
        fireDamage = damage;
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
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "cast") {
            //add interface for damamge here, IDamageable
           if(collision.gameObject.GetComponent<IDamageable>() != null)
            {
                collision.gameObject.GetComponent<IDamageable>().dealDamage(fireDamage);
            } 


            Destroy(gameObject);
        }
	}
}
