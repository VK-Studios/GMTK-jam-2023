using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName ="New Dialogue Box", menuName ="Dialogue")]
public class DialogueInfo : ScriptableObject
{
	//https://www.youtube.com/watch?v=aPXvoWVabPY
	public ECharacter Character;
	public string text;
	public bool used;
	public Sound clip;

}
