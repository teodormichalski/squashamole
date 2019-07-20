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
    }

    // Update is called once per frame
    void Update()
    {
        if (receive)
        {
        	receive = false;
        	ReceiveDamage();
        }
    }

    void ReceiveDamage()
    {
    	damage++;
    	if (damage > maxDamage) 
    	{
    		dead = true;
    		return;
    	}
    	int newScarNumber = (int)(Mathf.Floor(Random.Range(0, maxDamage - damage)));
    	int i = 0;
    	while (i < newScarNumber || scarsOn[i % maxDamage]) i++;
    	scarsOn[i % maxDamage] = true;
    	scars[i % maxDamage].GetComponent<SpriteRenderer>().sortingOrder = 2;

    }
}
