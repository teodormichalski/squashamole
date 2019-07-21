using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quacker : MonoBehaviour
{

	public AudioClip[] cries;
	private AudioSource audioSource;
	private int prevCry = -1;

	void Start() {
		audioSource = gameObject.GetComponent<AudioSource> ();
	}

	public void Cry() {
		int cry;
		do {
			cry = Random.Range (0, cries.Length);
		} while (cry == prevCry);
		prevCry = cry;
		audioSource.clip = cries [cry];
		audioSource.Play ();
	}
}
