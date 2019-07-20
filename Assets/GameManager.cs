using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int maxLength;
	private int[] faceStats;
	private GameObject modelHair;

    // Start is called before the first frame update
    void Start()
    {
		FaceGenerator.Randomize ();
		modelHair = GameObject.Find ("ModelHair");
		faceStats = FaceGenerator.GenerateFace (33, maxLength);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
