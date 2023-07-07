using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 5f;
    public Rigidbody2D rb;
	Vector2 moveDirection = Vector2.zero;

	private PlayerControls input;
	private InputAction move;
	private InputAction fire;
	private InputAction dash;

	public Animator legsAnim;
	public Animator torsoAnim;
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

	private void Awake() {
		input = new PlayerControls();
	}

	private void Start() {
		activeMoveSpeed = movementSpeed;
		
	}

	// User input code init
	// Using new input system, not old one 
	private void OnEnable() {
		move = input.Player.Move;
		move.Enable();

		fire = input.Player.Fire;
		fire.Enable();
		fire.performed += Fire;

		dash = input.Player.Dash;
		dash.Enable();
		dash.performed += Dash;
	}

	private void OnDisable() {
		move.Disable();
		fire.Disable();
		dash.Disable();

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

			if (moveDirection.x != 0 || moveDirection.y != 0) {
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

		

		torsoAnim.SetFloat("Dir", updateAnims());

	}

	private float updateAnims() {
		mousePos = Mouse.current.position.value;

		mousePos.z = 10f;

		objPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objPos.x;
		mousePos.y = mousePos.y - objPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		Debug.Log(angle);


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

		Debug.Log(mDir);
		return mDir;

	}

	private void FixedUpdate() {
		//movement
		rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, 0);
	}


	private void Fire(InputAction.CallbackContext context) {

		if (atkCoolCounter <= 0) {
			torsoAnim.SetTrigger("attack");
			atkCoolCounter = atkCooldown;
		}
		
	}

	private void Dash(InputAction.CallbackContext context) {
		
		if(dashCoolCounter <= 0 && dashCounter <= 0) {
			activeMoveSpeed = dashSpeed;
			dashCounter = dashLength;
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

}
