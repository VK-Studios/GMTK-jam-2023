using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour, IDamageable
{

    public float movementSpeed = 5f;
    public Rigidbody2D rb;
	Vector2 moveDirection = Vector2.zero;


	[SerializeField] private LayerMask m_WhatIsGround;
	[SerializeField] private Transform m_GroundCheck;
	[SerializeField] private float m_JumpForce = 400f;
	const float k_GroundedRadius = 1f;
	private bool m_Grounded;
	

	private Rigidbody2D m_Rigidbody2D;
	[Header("Events")]
	[Space]
	public UnityEvent OnLandEvent;

	private bool jump;

	private PlayerControls input;
	private InputAction move;
	private InputAction fire;
	private InputAction dash;
	private InputAction slot1;

	private InputAction jumpp;

	public Animator legsAnim;
	public Animator torsoAnim;
	public Animator effectAnim;
	private float dir = 1f;
	private float mDir = 1f;

	//dash
	Vector2 dashDirection = Vector2.zero;

	private float activeMoveSpeed;
	public float dashSpeed;

	public float dashLength = .5f;
	public float dashCooldown = 1f;

	private float dashCounter;
	private float dashCoolCounter;

	private Vector3 mousePos;
	private Vector3 objPos;
 
	public float atkCooldown = .5f;
	private float atkCoolCounter = 0;
	public int atkDamage;

	public pointAtk pointatk;
	public respawnw respawn;


	public int Maxhealth = 30;
	public int health;

	public int spendableSoul;


	private void Awake() {

		

		input = new PlayerControls();
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<respawnw>();
		spendableSoul = respawn.totalSoulOrbs;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

	}

	private void Start() {
		activeMoveSpeed = movementSpeed;
		health = Maxhealth;
		
	}

	// User input code init
	// Using new input system, not old one 
	private void OnEnable() {
		move = input.Player.Move;
		move.Enable();

		fire = input.Player.Fire;
		fire.Enable();
		fire.performed += Fire;

		slot1 = input.Player.Slot1;
		slot1.Enable();
		slot1.performed += Slot1;

		dash = input.Player.Dash;
		dash.Enable();
		dash.performed += Dash;

		jumpp = input.Player.Jump;
		jumpp.Enable();
		jumpp.performed += Jump;
	}

	private void OnDisable() {
		move.Disable();
		fire.Disable();
		dash.Disable();
		jumpp.Disable();
		slot1.Disable();
	}

	// Update is called once per frame
	void Update() {
		
		if (dashCounter == dashLength) {
			dashDirection = move.ReadValue<Vector2>();
		}
		
		if (dashCounter > 0) {
			dashCounter -= Time.deltaTime;

			
			moveDirection = dashDirection;

			if (dashCounter <= 0) {
				activeMoveSpeed = movementSpeed;
				dashCoolCounter = dashCooldown;
			}

		} else {
			//IMPORTANT MOVEMENT CODE 
			moveDirection = move.ReadValue<Vector2>();

			legsAnim.SetInteger("xInput", Mathf.RoundToInt(moveDirection.x));
			Debug.Log(Mathf.RoundToInt(moveDirection.x));

			/*if (moveDirection.x == 0 && moveDirection.y >= 0.01) {
				//up
				dir = 1;
				//Debug.Log("1");
			} else if (moveDirection.x == 0 && moveDirection.y <= -0.01) {
				//down
				dir = 2;
				//Debug.Log("2");} else */


			if (moveDirection.x >= 0.01) {
				//right
				dir = 3;
				//Debug.Log("3");
			} else if (moveDirection.x <= -0.01) {
				//left
				dir = 4;
				//Debug.Log("4");
			}

			legsAnim.SetFloat("lastInput", dir);

			if (moveDirection.x != 0) {
				legsAnim.SetBool("isMoving", true);
			} else {
				legsAnim.SetBool("isMoving", false);
			}
		}

		if(dashCoolCounter > 0) {
			dashCoolCounter -= Time.deltaTime;
		}

		if (atkCoolCounter > 0) {
			atkCoolCounter -= Time.deltaTime;
		}
		if(atkCoolCounter <= 0)
		{
			pointatk.frozen = false;
			pointatk.DisableAttack();
		}

		float mdir = updateAnims();

		torsoAnim.SetFloat("Dir", mdir);
		legsAnim.SetFloat("mouseDir", mdir);

		if (jump) {
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			jump = false;
		}
	}

	private float updateAnims() {
		mousePos = Mouse.current.position.value;

		mousePos.z = 10f;

		objPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objPos.x;
		mousePos.y = mousePos.y - objPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		//Debug.Log(angle);


		if (angle >= 90 || angle < -90) {
			mDir = 4;
		} else if (angle < 90 || angle >= -90) {
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

	private void FixedUpdate() {
		//movement
		rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, rb.velocity.y);

		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject) {
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	private void Fire(InputAction.CallbackContext context) {
		pointatk.EnableAttack();
		pointatk.frozen = true;
		if (atkCoolCounter <= 0) {
			torsoAnim.SetTrigger("attack");
			effectAnim.SetTrigger("attack");
			atkCoolCounter = atkCooldown;
		}
		
	}

	private void Slot1(InputAction.CallbackContext context) {

		if (atkCoolCounter <= 0) {
			torsoAnim.SetTrigger("channel");
			effectAnim.SetTrigger("fireball");
			atkCoolCounter = atkCooldown;
		}

	}

	private void Dash(InputAction.CallbackContext context) {
		
		if(dashCoolCounter <= 0 && dashCounter <= 0) {
			activeMoveSpeed = dashSpeed;
			dashCounter = dashLength;
		}

	}

	private void Jump(InputAction.CallbackContext context) {

		if (m_Grounded) {
			jump = true;
		}

	}

	public void setFrozen(bool froz) {
		if (froz) {
			move.Disable();
		}
        else
        {
			move.Enable();
        }
    }

    

    public void dealDamage(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
			respawn.death();
        }
    }
}
