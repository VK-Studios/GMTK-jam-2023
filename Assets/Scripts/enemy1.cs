using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class enemy1 : MonoBehaviour, IDamageable
{
    public int maxHealth = 10;
    public int heatlh;

    public float moveSpeed = 2f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDir;
    public int cooldown = 0;
    private Vector3 effectPos;
    public GameObject fireball;

    public Animator legsAnim;
    public Animator torsoAnim;
    public Animator effectAnim;

    private Vector3 mousePos;

    private Vector3 objPos;
    private float dir_ = 1f;
    private float mDir = 1f;

    public bool atTarget = false;

    public int abilCooldown = 0;
    public int ability = 0;


    public enemy1Atk pointatk;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void dealDamage(int damage)
    {
        heatlh = heatlh - damage;
        if (heatlh <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Slot1()
    {
            torsoAnim.SetTrigger("channel");
            effectAnim.SetTrigger("fireball");

    }

    private float updateAnims()
    {
        mousePos = target.position;


        objPos = gameObject.transform.position;
        mousePos.x = mousePos.x - objPos.x;
        mousePos.y = mousePos.y - objPos.y;
        mousePos.z = mousePos.z - objPos.z;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);


        if (angle >= 90 || angle < -90)
        {
            mDir = 4;
        }
        else if (angle < 90 || angle >= -90)
        {
            mDir = 3;
        }


        /*if (angle > -45 && angle < 45) {
			mDir = 1;
		} else if (angle >= 45 && angle <= 135) {
			mDir = 4;
		} else if (angle > 135 || angle < -135) {
			mDir = 2;
		} else if (angle <= -45 && angle >= -135) {
			mDir = 3;
		}*/

        return mDir;

    }


    // Start is called before the first frame update
    void Start()
    {
        heatlh = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if(abilCooldown >500)
        {
            System.Random random = new System.Random();
            ability = random.Next(0, 2);
            abilCooldown = 0;
        }
        abilCooldown++;

        Vector3 target1 = target.position;
        if(Math.Abs(target1.x - transform.position.x) <1)
        {
            atTarget = true;
        }
        else
        {
           atTarget=false;
        }

        if (target)
        {
            if (ability == 0)
            {

                if (target1.x - transform.position.x > 0)
                {
                    target1.x = target1.x - 3;

                }
                else
                {
                    target1.x = target1.x + 3;

                }
            }
            Vector3 dir = (target1 - transform.position).normalized;
            float angle = 0;
            rb.rotation = angle;
            moveDir = dir;


            legsAnim.SetInteger("xInput", Mathf.RoundToInt(moveDir.x));

            /*if (moveDirection.x == 0 && moveDirection.y >= 0.01) {
				//up
				dir = 1;
				//Debug.Log("1");
			} else if (moveDirection.x == 0 && moveDirection.y <= -0.01) {
				//down
				dir = 2;
				//Debug.Log("2");} else */


            if (moveDir.x >= 0.01)
            {
                //right
                dir_ = 3;
                //Debug.Log("3");
            }
            else if (moveDir.x <= -0.01)
            {
                //left
                dir_ = 4;
                //Debug.Log("4");
            }

            legsAnim.SetFloat("lastInput", dir_);

            if (moveDir.x != 0)
            {
                legsAnim.SetBool("isMoving", true);
            }
            else
            {
                legsAnim.SetBool("isMoving", false);
            }

            float mdir = updateAnims();

            torsoAnim.SetFloat("Dir", mdir);
            legsAnim.SetFloat("mouseDir", mdir);
           

        }
    }


    private void Fire()
    {
        pointatk.EnableAttack();
        pointatk.frozen = true;
            torsoAnim.SetTrigger("attack");
            effectAnim.SetTrigger("attack");
            
        

    }


    private void FixedUpdate()
    {

        rb.velocity = new Vector2(moveDir.x * moveSpeed, rb.velocity.y);
        if (atTarget)
        {
            cooldown++;
            if (cooldown > 27)
            {
                Fire();
                cooldown = 0;
            }
        }
        if(ability == 1)
        {
            cooldown++;
            if (cooldown > 27)
            {
                Fire();
                cooldown = 0;
            }
        }
        else if (ability == 0)
        {
            cooldown++;
            if (cooldown > 60)
            {
                Slot1();
                cooldown = 0;
            }
        }

    }


}
