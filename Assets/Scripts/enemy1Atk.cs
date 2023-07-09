using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class enemy1Atk : MonoBehaviour
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


    Transform target;
    public int cooldown = 0;

   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        target = GameObject.FindGameObjectWithTag("Player").transform;
        angle = Mathf.Atan2(target.position.x, (float)(transform.position.y)) * Mathf.Rad2Deg;

       transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90 ));
    
        effectPos = transform.position;


        if (Math.Abs(target.position.x - effectPos.x) < 5)
        {
            angle = Mathf.Atan2(target.position.x, (float)(transform.position.y)) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
        }

    }


    public void EnableAttack()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DisableAttack()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void fireTheBall()
    {
        Instantiate(fireball, effectPos, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag != "Enemy" && collision.gameObject.name != "Interaction Range" && collision.gameObject.tag != "cast")
        {

            //add interface for damamge here, IDamageable
            if (collision.gameObject.GetComponent<IDamageable>() != null)
            {
                collision.gameObject.GetComponent<IDamageable>().dealDamage(5);
            }

        }
    }

}
