using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
	public GameObject prevSegment;
	public float threeshold = 0.14f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
		if (Vector3.Distance (gameObject.transform.position, prevSegment.transform.position) > threeshold) {
			gameObject.transform.position = Vector3.MoveTowards (prevSegment.transform.position, gameObject.transform.position, threeshold);
		}
    }
}
