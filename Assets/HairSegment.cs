using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairSegment : MonoBehaviour
{
	public bool cut;
	public int id;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (cut) {
			Cut ();
		}
    }

	void Cut() {
		//transform.parent.gameObject.getCompontent<HairSpriteRenderer> ().Cut(id);
		Destroy (gameObject);
	}
}
