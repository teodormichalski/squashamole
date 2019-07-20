using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hair : MonoBehaviour {

	private LineRenderer lineRenderer;
	private List<Transform> children;
	private Vector3[] vertices;
	private int length;
	public GameObject lastSegment;
	public GameObject hairBulbPrefab;
	public GameObject hairSegmentPrefab;
	public bool test_grow;

	// Use this for initialization
	void Start () {
		lineRenderer = this.GetComponent<LineRenderer> ();
		children = transform.Cast<Transform> ().ToList();
		vertices = children.Select (i => i.position).ToArray();
		length = children.Count ();
		lineRenderer.positionCount = vertices.Length;
		lineRenderer.SetPositions (vertices);
	}
	
	// Update is called once per frame
	void Update () {
		if (test_grow) {
			test_grow = false;
			Grow ();
		}
	}

	void LateUpdate() {
		vertices = children.Select (i => i.position).ToArray ();
		lineRenderer.positionCount = length;
		lineRenderer.SetPositions (vertices);
	}

	public void Cut(int id) {
		length = id - 1;
		GameObject bulb = Instantiate (hairBulbPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		foreach (Transform child in children) {
			child.gameObject.GetComponent<HairSegment>().Cut (id, bulb);
		}
		children = children.Where (i => i.gameObject.GetComponent<HairSegment> ().id < id).ToList();
	}

	public void Grow() {
		lastSegment.gameObject.GetComponent<HairSegment> ().Grow (hairSegmentPrefab);
	}

	public void RegisterNewChild(Transform child) {
		length++;
		children.Add (child);
		RegisterNewEnding (child.gameObject);
	}

	public void RegisterNewEnding(GameObject child) {
		lastSegment = child;
	}
}
