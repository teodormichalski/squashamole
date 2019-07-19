using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HairSpriteRenderer : MonoBehaviour {

	private LineRenderer lineRenderer;
	private Transform[] children;
	private Vector3[] vertices;
	private int length;

	// Use this for initialization
	void Start () {
		lineRenderer = this.GetComponent<LineRenderer> ();
		children = transform.Cast<Transform> ().ToArray();
		Rigidbody2D last = null;
		foreach(Transform child in children) {
			if (last != null) {
				child.GetComponent<FrictionJoint2D> ().connectedBody = last;
			}
			last = child.GetComponent<Rigidbody2D> ();
		}
		vertices = children.Select (i => i.position).ToArray();
		length = children.Length;
		lineRenderer.positionCount = vertices.Length;
		lineRenderer.SetPositions (vertices);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate() {
		vertices = children.Select (i => i.position).ToArray ();
		lineRenderer.positionCount = vertices.Length;
		lineRenderer.SetPositions (vertices);
	}

	void Cut(int id) {
		length = id - 1;
	}
}
