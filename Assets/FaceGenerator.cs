using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FaceGenerator
{
	private static int flatCount = 1;
	private static int threeshold = 5;
	private static float changeChance = 0.5f; 
	private static int minLength = 30;

	private static List<int[]> objectives = new List<int[]>();

	private static List<GameObject> posters = new List<GameObject>();

	private static GameObject poster;

	public static void Randomize() {
		Random.InitState((int)System.DateTime.Now.Ticks);
		int[] values;
		values = new int[34];
        //Grzywka lewo
        for (int i = 0; i < 34; i++)
        {
            values[i] = 3 + i;
        }
		objectives.Add (values);
		poster = GameObject.Find ("goal 2");
		poster.SetActive (false);
		posters.Add (poster);
        //Grzywka prawo
		values = new int[34];
        for (int i = 0; i < 34; i++)
        {
            values[i] = 34 - i;
        }
		objectives.Add (values);
		poster = GameObject.Find ("goal 6");
		poster.SetActive (false);
		posters.Add (poster);
        //Prawe pasemko dłuższe
		values = new int[34];
        for (int i = 0; i < 34; i++)
        {
            values[i] = 15;
        }
       	for (int i = 0; i < 10; i++)
        {
            values[i + 24] = 50 - i / 2;
        }
		objectives.Add (values);
		poster = GameObject.Find ("goal 8");
		poster.SetActive (false);
		posters.Add (poster);
        //Prosta Grzywka
		values = new int[34];
        for (int i = 0; i < 17; i++) {
            values[i] = 20 + i;
        }
        for (int i = 0; i < 17; i++)
        {
            values[i + 17] = 35 - i;
        }
		objectives.Add (values);
        //Grzywka okrągła
		values = new int[34];
        for (int i = 0; i < 34; i++)
        {
            values[i] = 10;
        }
		objectives.Add (values);
		poster = GameObject.Find ("goal 7");
		poster.SetActive (false);
		posters.Add (poster);
        //Lewo i prawo pasemko dłuższe
		values = new int[34];
        for (int i = 0; i < 34; i++)
        {
            values[i] = 50 + i / 2;
        }
        for (int i = 0; i < 17; i++)
        {
            values[i + 13] = 15;
        }
        for (int i = 0; i < 10; i++)
        {
            values[i + 24] = 60 - i / 2;
        }
        objectives.Add(values);
		poster = GameObject.Find ("goal 4");
		poster.SetActive (false);
		posters.Add (poster);
	}

	public static int[] GenerateFace(int hairCount, int maxLength) {
		int[] faceStats = new int[hairCount];
		faceStats [0] = Random.Range (0, maxLength+1);
		for (int i = 1; i < hairCount; i++) {
			if (flatCount < threeshold) {
				faceStats [i] = Mathf.Max(faceStats [i - 1] + Random.Range(-1, 2), minLength);
				flatCount++;
			} else {
				if (Random.Range (0f, 1f) > changeChance) {
					flatCount = 1;
					faceStats [i] = Random.Range (0, maxLength+1);
				} else {
					flatCount++;
					faceStats [i] = Mathf.Max(faceStats [i - 1] + Random.Range(-1, 2), minLength);
				}
			}
		}
		return faceStats;
	}

	public static int[] GetRandomObjective() {
		int choice = Random.Range (0, objectives.Count);
		Debug.Log (choice);
		posters [choice].SetActive (true);
		return objectives [choice];
	}
}
