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
	public GameObject head;
	public GameObject firstSegment;
	public bool test_grow;
	public int growThreeshold;
	public int minThreeshold;
	public int maxThreeshold;
	private int growCounter;

	// Use this for initialization
	void Start () {
		if (head == null)
			head = GameObject.Find ("Head");
		if (firstSegment == null)
			firstSegment = gameObject.transform.GetChild (0).gameObject;
		lineRenderer = this.GetComponent<LineRenderer> ();
		children = transform.Cast<Transform> ().ToList();
		vertices = children.Select (i => i.position).ToArray();
		growCounter = 0;
		if (growThreeshold <= 0) growThreeshold = Random.Range (minThreeshold, maxThreeshold);
		length = children.Count ();
		lineRenderer.positionCount = vertices.Length;
		lineRenderer.SetPositions (vertices);
		firstSegment.GetComponent<DistanceJoint2D> ().connectedBody = head.GetComponent<Rigidbody2D>();
		firstSegment.GetComponent<DistanceJoint2D> ().connectedAnchor = new Vector2 (firstSegment.transform.position.x, firstSegment.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		growCounter++;
		if (test_grow) {
			test_grow = false;
			Grow ();
		}
		if ((growCounter >= growThreeshold) && (length <= 1)) {
			growCounter = 0;
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
