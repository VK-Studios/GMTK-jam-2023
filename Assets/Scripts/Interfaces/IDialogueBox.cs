using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueBox
{
    public void OnCollision();
    public void nextDialogue();

    public bool getActive();
    public bool getComplete();

    public void setActive(bool active);
    public void setComplete(bool complete);

}
