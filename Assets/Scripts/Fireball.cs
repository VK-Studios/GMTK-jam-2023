using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fireball : MonoBehaviour
{

    private Rigidbody2D rb;
    private int life = 200;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        life -= 1;

        if (life <= 0) {
            Destroy(gameObject);
        }
    }

	private void FixedUpdate() {
		rb.velocity = new Vector2(transform.up.y * 10, -transform.up.x * 10);
	}


	private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") {
            //add interface for damamge here, IDamageable

            Destroy(gameObject);
        }
	}
}
