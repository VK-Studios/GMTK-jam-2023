using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using Unity.VisualScripting;

public class interactionBox : MonoBehaviour {

	private PlayerControls input;
	private InputAction interact;

	// note to self: add contingancies for multiple interactables in range!
	// Hey, future self here! doing that now, thanks for the reminder.

	private List<GameObject> _gameObjectInteractablesInRange = new List<GameObject>();

	//private List<IInteractable> _interactablesInRange = new List<IInteractable>();

	[SerializeField] private Transform playerTransform;

	private float interDistance;

	private GameObject closestItem;

	[SerializeField] private GameObject fText;

	void Awake() {
		Object[] tmpro = Resources.FindObjectsOfTypeAll(typeof(TextMeshProUGUI));
		for (int i = 0; i < tmpro.Length; i++) {
			if (tmpro[i].GameObject().tag == "fText") {
				fText = tmpro[i].GameObject();
				break;
			}
		}

		interDistance = 0;
		input = new PlayerControls();
	}

	private void OnEnable() {
		interact = input.Player.Interact;
		interact.Enable();
		interact.performed += Interact;
	}

	private void OnDisable() {
		interact.Disable();
	}


	private GameObject findClosestInter() {
		interDistance = 0;
		for (int i = 0; i < _gameObjectInteractablesInRange.Count; i++) {
			float dist = Vector2.Distance(_gameObjectInteractablesInRange[i].transform.position, playerTransform.position);

			if (i == 0) {
				interDistance = dist;
				closestItem = _gameObjectInteractablesInRange[i];
			} else if (i > 0 && dist < interDistance) {
				interDistance = dist;
				closestItem = _gameObjectInteractablesInRange[i];
			}
		}

		return closestItem;
	}

	// Update is called once per frame
	void Update() {
		//to run dialogue, must be inside trigger, press e, NPC must be interactable, and there shouldn't already be dialogue running
		//Debug.Log(insideBox + ", " + interactable + ", ");
		if (_gameObjectInteractablesInRange.Count > 0) {

			fText.GetComponent<TextMeshProUGUI>().text = "Press 'E' to Interact with " + findClosestInter().name;

		}

	}

	void OnTriggerEnter2D(Collider2D other) {

		var interable = other.gameObject;

		if (interable.GetComponent<IInteractable>() != null) {
			if (interable.GetComponent<IInteractable>().canInteract() == true) {

				_gameObjectInteractablesInRange.Add(interable);

				fText.SetActive(true);

				/*
                IInsideBox = true;
                eText.SetActive(true);
                interactedObject = other.gameObject;
                */
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {

		var interable = other.gameObject;

		if (interable.GetComponent<IInteractable>() != null) {

			if (_gameObjectInteractablesInRange.Contains(interable)) {

				_gameObjectInteractablesInRange.Remove(interable);

				if (_gameObjectInteractablesInRange.Count == 0) {
					fText.SetActive(false);
				}
			}


			/*
                IInsideBox = false;
                eText.SetActive(false);
                interactedObject = null;
            */
		}


		/*GameObject item = col.gameObject;

        if (item.GetComponent<IInteractable>() != null) {
            IInsideBox = false;
            eText.SetActive(false);
            interactedObject = null;
        }*/
	}

	private void Interact(InputAction.CallbackContext context) {

		if (_gameObjectInteractablesInRange.Count == 1) {

			var interactObject = _gameObjectInteractablesInRange[0];
			interactObject.GetComponent<IInteractable>().Interact();



			if (interactObject.GetComponent<IInteractable>().canInteract() == false) {

				_gameObjectInteractablesInRange.Remove(interactObject);
				fText.SetActive(false);
			}
		} else if (_gameObjectInteractablesInRange.Count > 1) {

			var interactObject = findClosestInter();
			interactObject.GetComponent<IInteractable>().Interact();


			if (interactObject.GetComponent<IInteractable>().canInteract() == false) {

				_gameObjectInteractablesInRange.Remove(interactObject);

				fText.SetActive(false);
			}


		}

	}


}
