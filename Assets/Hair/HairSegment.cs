﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairSegment : MonoBehaviour
{
	public bool test_cut;
	public int id;
	public Hair hairBulb;

    // Start is called before the first frame update
    void Start()
    {
		hairBulb = transform.parent.gameObject.GetComponent<Hair> ();
    }

    // Update is called once per frame
    void Update()
    {
		if (test_cut) {
			hairBulb.Cut(id);
		}
		if (transform.position.y <= -10)
			hairBulb.Die ();
    }

	public void GetCut() {
		if(id != 0) hairBulb.Cut (id);
	}

	public void Cut(int id, GameObject bulb) {
		if (id == this.id) {
			Destroy (gameObject);
		}
		if (id < this.id) {
			gameObject.transform.parent = bulb.transform;
			hairBulb = bulb.GetComponent<Hair> ();
		}
		if (id == this.id + 1) {
			hairBulb.RegisterNewEnding (this.gameObject);
		}
	}

	public void Grow(GameObject hairSegmentPrefab) {
		GameObject newSegment = Instantiate (hairSegmentPrefab, new Vector3(this.transform.position.x, this.transform.position.y - 0.14f, this.transform.position.z), Quaternion.identity);
		newSegment.GetComponent<HairSegment> ().id = id + 1;
		newSegment.transform.parent = transform.parent;
		newSegment.GetComponent<HairSegment> ().hairBulb = hairBulb;
		newSegment.GetComponent<DistanceJoint2D> ().connectedBody = gameObject.GetComponent<Rigidbody2D> ();
		hairBulb.RegisterNewChild (newSegment.transform);
	}
		
}
