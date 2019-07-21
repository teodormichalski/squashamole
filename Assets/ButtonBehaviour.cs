using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
	public int state;
	public int previousState;
	public float lastTick;
	public Button button;
    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        previousState = -1;
        foreach (Transform child in gameObject.transform)
        	child.gameObject.SetActive(false);
        button.onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Update()
    {
    	if (state >= 1 && state < 17 && Time.time - lastTick >= 1.0f)
    		IncrementCountdown();
    	if (state >= gameObject.transform.childCount)
    		EndGame();
        if (previousState != state) 
        {
        	if(previousState >= 0)
	        	gameObject.transform.GetChild(previousState).gameObject.SetActive(false);
        	gameObject.transform.GetChild(state).gameObject.SetActive(true);
        	previousState = state;
        }
    }

    void IncrementCountdown() {
		lastTick = Time.time;
		state++;
		if (state == 1) lastTick -= 0.9f;
    }

    void EndGame() {
    	state = 0;
  		//tu punktacja
    }

    void Click() {
    	if (state == 0)
    		IncrementCountdown();
    }


}
