using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 pos2;
    private float width;
    private float height;
    TrailRenderer trail;
    int Counter;
    Ray castpoint;

    void Start()
    {
        trail = gameObject.GetComponentInChildren<TrailRenderer>();
    }


    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    trail.enabled = false;
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    trail.enabled = true;
                    Vector3 mouse = Input.mousePosition;
                    castpoint = Camera.main.ScreenPointToRay(mouse);
                    transform.position = castpoint.origin;
                }
            }
        }
        else
        {
            castpoint = Camera.main.ScreenPointToRay(Input.mousePosition);
            transform.position = castpoint.origin;
            RaycastHit2D hit = Physics2D.Raycast(castpoint.origin,castpoint.direction);

                
        }
        Counter++;
		foreach (var hair in GameObject.FindGameObjectsWithTag("HairSegment"))
        {
            float distfromcur = Vector3.Distance(hair.GetComponent<Transform>().position, castpoint.origin);
            if (distfromcur < 9.71f && Counter > 1)
            {
				hair.GetComponent<HairSegment>().GetCut();
                Counter = 0;

            }
        }

    }
}
