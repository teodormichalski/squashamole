﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 pos2;
    private float width;
    private float height;
    TrailRenderer trail;
    bool once;
    int Counter;
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
                    Ray castpoint = Camera.main.ScreenPointToRay(mouse);
                    transform.position = castpoint.origin;
                }
            }
        }
        else
        {
            Vector3 mouse = Input.mousePosition;
            Ray castpoint = Camera.main.ScreenPointToRay(mouse);
            transform.position = castpoint.origin;
        }
        Counter++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Segment") && Counter > 5)
        {
            collision.gameObject.GetComponent<HairSegment>().test_cut = true;
            Debug.Log(collision.gameObject.GetComponent<HairSegment>().id);
            Counter = 0;
        }
        Debug.Log(collision.name);
        if (collision.gameObject.name.Contains("Head"))
        {
            collision.gameObject.GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("ebe");
        }

    }
}
