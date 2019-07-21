using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misbehaviour : MonoBehaviour
{
	public GameObject[] scars;
	public bool[] scarsOn;
	public int maxDamage;
	public int damage = 0;
	public bool dead = false;
	public bool receive;
	public bool cutLeft;
	public bool cutRight;
	public int mood = 1;

	public GameObject leftEar;
	public GameObject rightEar;
	public GameObject[] moodFeatures1;
	public GameObject[] moodFeatures2;
	public GameObject[] moodFeatures3;
	public GameObject[] moodFeatures4;
	public GameObject[] moodFeatures5;
	private GameObject[][] moodFeatures;
    // Start is called before the first frame update
    void Start()
    {
        scars = GameObject.FindGameObjectsWithTag("Scar");
        maxDamage = scars.Length;
        scarsOn = new bool[maxDamage];
        for (int i = 0; i < maxDamage; i++)
        {
        	scarsOn[i] = false;
        	scars[i].GetComponent<SpriteRenderer>().sortingOrder = -10;
        }
        mood = 1;
        moodFeatures = new GameObject[5][];
        moodFeatures[0] = moodFeatures1;
        moodFeatures[1] = moodFeatures2;
        moodFeatures[2] = moodFeatures3;
        moodFeatures[3] = moodFeatures4;
        moodFeatures[4] = moodFeatures5;
        ChangeMoodFeatures();

        leftEar = GameObject.Find("ear left");
        rightEar = GameObject.Find("ear right");
    }

    // Update is called once per frame
    void Update()
    {
        if (receive)
        {
        	receive = false;
        	ReceiveDamage();
        }
        if (cutRight) 
        {
        	cutRight = false;
        	CutEarRight();
        }
        if (cutLeft) 
        {
        	cutLeft = false;
        	CutEarLeft();
        }
    }

    void CutEarLeft() {
    	if (scarsOn[2]) 
    	{
    		damage++;
			if (damage > maxDamage) 
			{
				dead = true;
				return;
			}
    		LooseEar(leftEar);
    	} else {
    		ReceiveDamage(2);
    	}
    }

    void CutEarRight() {
    	if (scarsOn[3]) 
    	{
			if (dead)
				return;
    		damage++;
			if (damage > maxDamage) 
			{
				dead = true;
				return;
			}
    		LooseEar(rightEar);
    	} else {
    		ReceiveDamage(3);
    	}
    }

    void LooseEar(GameObject ear) {
    	GameObject.Destroy(ear.GetComponent<SliderJoint2D>());
    	ear.GetComponent<Rigidbody2D>().AddForce(Random.onUnitSphere * 10f);
    	ear.GetComponent<Rigidbody2D>().AddTorque(Random.value * 100f);
    }

    void ReceiveDamage()
    {
    	int newScarNumber = (int)(Mathf.Floor(Random.Range(0, maxDamage - damage)));
    	ReceiveDamage(newScarNumber);
    }

    void ReceiveDamage(int newScarNumber)
    {
		if (dead)
			return;
    	damage++;
    	if (damage % 4 == 0)
    	{
    		mood++;
    		ChangeMoodFeatures();
    	}
    	if (damage > maxDamage) 
    	{
    		dead = true;
    		return;
    	}
    	int i = 0;
    	while (i < newScarNumber || scarsOn[i % maxDamage]) i++;
    	scarsOn[i % maxDamage] = true;
    	scars[i % maxDamage].GetComponent<SpriteRenderer>().sortingOrder = 2;

    }

    void ChangeMoodFeatures() 
    {
    	foreach (GameObject[] features in moodFeatures)
    		foreach (GameObject f in features)
    			f.SetActive(false);
    	foreach (GameObject f in moodFeatures[mood - 1])
    		f.SetActive(true);
    }
}
