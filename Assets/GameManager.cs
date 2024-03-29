﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

	public int maxLength;
	public int hairCount;
	public int gameLength;
	public int[] faceStats;
    public int[] hairLenghts;
	private GameObject modelHair;
	public int[] objective;
	public GameObject cursor;
	public Misbehaviour mateuszek;
	public int score;
	public Text scoreUI;
	public GameObject head;
	private GameObject finish;
	public GameObject btnSprite;
	public Sprite button;
	public GameObject musicPlayer;
    // Start is called before the first frame update
    void Start()
    {
		if (GameObject.Find ("MusicPlayer(Clone)") == null) {
			GameObject tmp = Instantiate (musicPlayer, Vector3.zero, Quaternion.identity);
			DontDestroyOnLoad (tmp);
			tmp.GetComponent<AudioSource> ().Play ();
		}
		FaceGenerator.Randomize ();
		modelHair = GameObject.Find ("ModelHair");
		mateuszek = GameObject.Find ("Mateuszek").GetComponent<Misbehaviour>();
		scoreUI = GameObject.Find ("Score").GetComponent<Text> ();
		cursor = GameObject.Find ("Cursor");
		head = GameObject.Find ("head");
		finish = GameObject.Find ("Finish");
		btnSprite = GameObject.Find ("Button").transform.GetChild(0).gameObject;
		finish.SetActive (false);
		cursor.GetComponent<Track> ().SetEnabled (false);
    }

    private void Awake()
    {
        instance = this;
    }

    public void StartGame() {
		faceStats = FaceGenerator.GenerateFace (34, maxLength);
		//faceStats = FaceGenerator.GetRandomObjective();
		int index = 0;
		score = 2000;
		Hair hair;
		foreach (int length in faceStats) {
			hair = modelHair.transform.GetChild (2 * index).gameObject.GetComponent<Hair> ();
			for (int i = 0; i < length; i++) {
				hair.Grow ();
			}
			index++;
		}
		head.GetComponent<HeadBehaviour> ().isMoving = true;
		objective = FaceGenerator.GetRandomObjective ();
		cursor.GetComponent<Track> ().SetEnabled (true);
	}

	public void EndGame()
    {
		finish.SetActive (true);	
		head.GetComponent<HeadBehaviour> ().isMoving = false;
		cursor.GetComponent<Track> ().SetEnabled (false);
		score = EvaluateScore (faceStats, objective);
		scoreUI.text = score.ToString();
		btnSprite.GetComponent<SpriteRenderer> ().sprite = button;
		foreach (GameObject hair in GameObject.FindGameObjectsWithTag("HairParent")) {
			hair.GetComponent<Hair> ().alive = false;
		}
	}

	void Update() {
/*		if (Input.GetKeyDown (KeyCode.Return)) {
			StartGame ();
		}*/
	}

	int EvaluateScore(int[] faceStats, int[] objective) {
		int score = this.score;
		score = (int)((float)score * ((float)((mateuszek.maxDamage+1) - mateuszek.damage) / (float)(mateuszek.maxDamage+1)));
		for (int i = 0; i < 34; i++) {
			score -= Mathf.Abs (faceStats [i] - objective [i]);
		}
		return score;
	}
}
