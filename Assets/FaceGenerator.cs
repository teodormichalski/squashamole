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

	public static void Randomize() {
		Random.InitState((int)System.DateTime.Now.Ticks);
		int[] values;
		values = new int[34];
		float p = 50;
        //Grzywka lewo
        //for (int i = 0; i < 34; i++)
        //{
        //    values[i] = 3 + i;
        //}
        //Grzywka prawo
        //for (int i = 0; i < 34; i++)
        //{
        //    values[i] = 34 - i;
        //}
        //Prawe pasemko dłuższe
        //for (int i = 0; i < 34; i++)
        //{
        //    values[i] = 15;
        //}
        //for (int i = 0; i < 10; i++)
        //{
        //    values[i + 24] = 50 - i / 2;
        //}
        //Prosta Grzywka
        //for (int i = 0; i < 17; i++) {
        //    values[i] = 20 + i;
        //}
        //for (int i = 0; i < 17; i++)
        //{
        //    values[i + 17] = 35 - i;
        //}
        //Grzywka okrągła
        //for (int i = 0; i < 34; i++)
        //{
        //    values[i] = 10;
        //}
        //
        //Lewo i prawo pasemko dłuższe
        //for (int i = 0; i < 34; i++)
        //{
        //    values[i] = 50 + i / 2;
        //}
        //for (int i = 0; i < 17; i++)
        //{
        //    values[i + 13] = 15;
        //}
        //for (int i = 0; i < 10; i++)
        //{
        //    values[i + 24] = 60 - i / 2;
        //}
        //for (int i = 0; i < 34; i++)
        //{
        //    p = 50;
        //    values[i] = (int)Mathf.Round(p);
        //}
        //      for (int i = 0; i < 10; i++) {
        //	values [i+24] = 20 - i - 1;
        //}
        objectives.Add(values);
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
		return objectives [Random.Range (0, objectives.Count)];
	}
}
