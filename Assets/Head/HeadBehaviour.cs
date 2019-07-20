using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBehaviour : MonoBehaviour
{
	private float sinBase = 0f;
	private float cosBase = 0f;
	public Vector3 jumpOrigin;
	public float jumpAmp = 0f;
	public float jumpDelay = 100f;
	public float jumpPhase = 0f;
	public float jumpCycle = 0f;
	Transform parentTf;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(42);
        jumpOrigin = GetComponent<Transform>().parent.position;
        parentTf = GetComponent<Transform>().parent;
    }

    // Update is called once per frame
    void Update()
    {
    	//Rigidbody2D rb = GetComponent<Rigidbody2D>();
    	//float horizontal = Mathf.PerlinNoise(Time.frameCount * 0.01f, 0f) - 0.5f;
    	//rb.AddForce(Vector3.left * horizontal * 5f);
    	RandomPivot();
    	JumpyNeck();
    	Sway();

		//Vector3 pos = parentTf.position;
		//parentTf.position = new Vector3(jumpOrigin.x + sin, pos.y, pos.z);

	}

	void RandomPivot()
	{
		Transform tf = GetComponent<Transform>();
    	sinBase += Mathf.PerlinNoise(Time.frameCount * 0.001f, 0f) * 0.5f;
    	cosBase += Mathf.PerlinNoise(0f, Time.frameCount * 0.001f) * 5.0f;
    	float rot = Mathf.Sin(sinBase);
    	float amp = Mathf.Cos(cosBase) + 1f;
    	rot *= amp;

    	if (tf.eulerAngles.z < -35 && tf.eulerAngles.z > -180) rot = 1f; 
    	if (tf.eulerAngles.z < 315 && tf.eulerAngles.z > 180) rot = 1f; 
    	if (tf.eulerAngles.z > 35 && tf.eulerAngles.z < 180) rot = -1f;
    	if (tf.eulerAngles.z > -315 && tf.eulerAngles.z < -180) rot = -1f;


		tf.RotateAround(tf.parent.position, Vector3.forward, rot);
	} 

	void JumpyNeck()
	{
		if (jumpDelay > 0)
			jumpDelay -= Mathf.PerlinNoise(10f, Time.frameCount * 0.01f);
    	else if (jumpDelay < 0)
    	{
    		jumpPhase = 0f;
    		jumpDelay = 0f;
    		jumpAmp = Random.value * 1f + 1f;
    		jumpCycle = Random.value * 20f + 10f;
    	}
    	else if (jumpDelay == 0) 
    	{
			jumpPhase += 1f;
			float move = Mathf.Sin(Mathf.PI * jumpPhase / jumpCycle) * jumpAmp;
			if (jumpPhase >= jumpCycle) jumpDelay = Random.value * 120f;
			Vector3 pos = parentTf.position;
			parentTf.position = new Vector3(pos.x, jumpOrigin.y + move, pos.z);
			//return jumpOrigin.y + move;
		}
		//return 0f;

	}

	void Sway()
	{
		float phase = Mathf.PerlinNoise(Time.frameCount * 0.001f, 0f) -0.5f;
		float sin = Mathf.Sin(sinBase * 0.4f + phase);
		Vector3 pos = parentTf.position;
		parentTf.position = new Vector3(jumpOrigin.x + sin, pos.y, pos.z);		
		//return jumpOrigin.x + sin;
	}
}
