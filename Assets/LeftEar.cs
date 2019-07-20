using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftEar : MonoBehaviour
{
    
	GameObject receiver;

    void Start()
    {
		receiver = GameObject.Find ("Neck");
    }

	void OnTriggerEnter2D(Collider2D other) {
		
	}
}
