using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int maxLength;
	public int hairCount;
	public int gameLength;
	public int[] faceStats;
    public int[] hairLenghts;
	private GameObject modelHair;
	public int[] objective;
    // Start is called before the first frame update
    void Start()
    {
		FaceGenerator.Randomize ();
		modelHair = GameObject.Find ("ModelHair");
		//faceStats = FaceGenerator.GenerateFace (34, maxLength);
		faceStats = FaceGenerator.GetRandomObjective();
		int index = 0;
		Hair hair;
		foreach (int length in faceStats) {
			hair = modelHair.transform.GetChild (2 * index).gameObject.GetComponent<Hair>();
            
			for (int i = 0; i < length; i++) {
				hair.Grow ();
			}
			index++;
		}
    }

	void StartGame() {
		objective = FaceGenerator.GetRandomObjective ();
		Invoke ("EndGame", gameLength);

	}

	void EndGame() {
		
	}

    // Update is called once per frame
    void Update()
    {
    }
}
