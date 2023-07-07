using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    //Called when interaction input is pressed on this object
    public void Interact();

    //Returns whether or not an interactable object can be interacted with via boolean
    public bool canInteract();
}
