using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTester : MonoBehaviour {

	public float speed = 1f;
	public float moved = 0f;
	public float amptitude = 5f;
	public float direction = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float distance = speed * direction * Time.deltaTime;
		if (Mathf.Abs (moved) >= Mathf.Abs (amptitude)) {
			distance *= -1;
			direction *= -1;
		}
		moved += distance;
		transform.Translate (new Vector2 (distance, 0));
	}

	void FixedUpdate() {
	}
}
