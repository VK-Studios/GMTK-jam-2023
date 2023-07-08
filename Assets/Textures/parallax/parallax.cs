using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour {
	private float length, startpos;
	public GameObject cam;
	public float effect;
	// Start is called before the first frame update
	void Start() {
		startpos = transform.position.x;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
	}

	void FixedUpdate() {
		float temp = (cam.transform.position.x * (1 - effect));
		float dist = (cam.transform.position.x * effect);

		transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

		if (temp > startpos + length - 5) {
			startpos += length;

		} else if (temp < startpos - length + 5) {
			startpos -= length;
		}
	}
}
