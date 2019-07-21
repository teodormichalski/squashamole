using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
		FaceGenerator.Randomize ();
		modelHair = GameObject.Find ("ModelHair");
		mateuszek = GameObject.Find ("Mateuszek").GetComponent<Misbehaviour>();
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
		objective = FaceGenerator.GetRandomObjective ();
		cursor.SetActive (true);
	}

	public void EndGame()
    {
		cursor.SetActive (false);
		Debug.Log ("GAme end");
		score = EvaluateScore (faceStats, objective);
	}

	void Update() {
/*		if (Input.GetKeyDown (KeyCode.Return)) {
			StartGame ();
		}*/
	}

	int EvaluateScore(int[] faceStats, int[] objective) {
		int score = this.score;
		score = (int)((float)score * Mathf.Sqrt((float)((mateuszek.maxDamage+1) - mateuszek.damage) / (float)(mateuszek.maxDamage+1)));
		Debug.Log (score);
		for (int i = 0; i < 34; i++) {
			score -= Mathf.Abs (faceStats [i] - objective [i]);
		}
		Debug.Log (score);
		return score;
	}
}
