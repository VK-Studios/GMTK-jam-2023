using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shop : MonoBehaviour, IInteractable
{	

	public bool interactionOn = true;
	SpriteRenderer spriteRenderer;

	public bool canInteract() {
		return interactionOn;
	}

	public void Interact() {
		Debug.Log("Yay, it's shopping time!");
		
		spriteRenderer.color = Color.green;
	}

	// Start is called before the first frame update
	void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();

	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
