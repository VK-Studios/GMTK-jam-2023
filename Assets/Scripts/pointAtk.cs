using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class pointAtk : MonoBehaviour
{

    public GameObject attack;

    private Vector3 mousePos;
	public Transform atkLoc;
	private Vector3 objPos;
	private float angle;
	private Vector3 effectPos;

	private float tan;
	private bool rotating = true;

	public GameObject fireball;
	public bool frozen = false;

    public int swordDamage =5;
    public int damageOfFire = 5;

	// Start is called before the first frame update
	void Start()
    {
        swordDamage = 5;
        damageOfFire = 5;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(swordDamage);
        mousePos = Mouse.current.position.value;

		mousePos.z = 10f;

		objPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objPos.x;
		mousePos.y = mousePos.y - objPos.y;

		angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

		if (!frozen)
		{
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}
		effectPos = transform.position;
	
	}


    public void EnableAttack()
    {
	    gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
	
	public void DisableAttack()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void fireTheBall() {

		GameObject FIREEEE = Instantiate(fireball, effectPos, transform.rotation);
        FIREEEE.GetComponent<Fireball>().setDamage(damageOfFire);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            //add interface for damamge here, IDamageable
            if (collision.gameObject.GetComponent<IDamageable>() != null)
            {
                collision.gameObject.GetComponent<IDamageable>().dealDamage(swordDamage);
            }

        }
    }

}
