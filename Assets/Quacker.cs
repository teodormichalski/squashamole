using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quacker : MonoBehaviour
{

	public AudioClip[] cries;
	private AudioSource audioSource;

	void Start() {
		audioSource = gameObject.GetComponent<AudioSource> ();
	}

	public void Cry() {
		audioSource.clip = cries [Random.Range (0, cries.Length)];
		audioSource.Play ();
	}
}
