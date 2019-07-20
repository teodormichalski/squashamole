using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public int maxLength;
	private int[] faceStats;
    public int[] hairLenghts;
	private GameObject modelHair;
    private bool[] pointHair;
    // Start is called before the first frame update
    void Start()
    {
		FaceGenerator.Randomize ();
		modelHair = GameObject.Find ("ModelHair");
		faceStats = FaceGenerator.GenerateFace (39, maxLength);
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
        hairLenghts = new int[35];
        pointHair = new bool[35];
        for (int i = 0; i < 35; i++)
        {
            hairLenghts[i] = GameObject.FindGameObjectsWithTag("HairParent")[i].GetComponent<Transform>().childCount;

            if (hairLenghts[i] < 10)
            {
                pointHair[i] = true;
            }
            if (pointHair.All(x => x))
            {
                Debug.Log("ebebe");
            }
        }
    }
}
