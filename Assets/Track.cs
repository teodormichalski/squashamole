using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public float cutThreeshold;
    public float precision;
    private Vector3 pos2;
    private float width;
    private float height;
    TrailRenderer trail;
    Vector3 castpoint;
    Vector3 prevCastpoint = Vector3.zero;

	public float cooldown;
	private float cdnCounter;
	public GameObject leftEar;
	public GameObject rightEar;
	public GameObject nose;
	public GameObject neck;
	public Misbehaviour receiver;
	private bool enabled;
	private bool fix = false;

	public void SetEnabled(bool state) {
		enabled = state;
	}

    public static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point)
    {

        //get vector from point on line to point in space
        Vector3 linePointToPoint = point - linePoint;

        float t = Vector3.Dot(linePointToPoint, lineVec);

        return linePoint + lineVec * t;
    }

    public static int PointOnWhichSideOfLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {

        Vector3 lineVec = linePoint2 - linePoint1;
        Vector3 pointVec = point - linePoint1;

        float dot = Vector3.Dot(pointVec, lineVec);

        //point is on side of linePoint2, compared to linePoint1
        if (dot > 0)
        {

            //point is on the line segment
            if (pointVec.magnitude <= lineVec.magnitude)
            {

                return 0;
            }

            //point is not on the line segment and it is on the side of linePoint2
            else
            {

                return 2;
            }
        }

        //Point is not on side of linePoint2, compared to linePoint1.
        //Point is not on the line segment and it is on the side of linePoint1.
        else
        {

            return 1;
        }
    }

    public static Vector3 ProjectPointOnLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {

        Vector3 vector = linePoint2 - linePoint1;

        Vector3 projectedPoint = ProjectPointOnLine(linePoint1, vector.normalized, point);

        int side = PointOnWhichSideOfLineSegment(linePoint1, linePoint2, projectedPoint);

        //The projected point is on the line segment
        if (side == 0)
        {

            return projectedPoint;
        }

        if (side == 1)
        {

            return linePoint1;
        }

        if (side == 2)
        {

            return linePoint2;
        }

        //output is invalid
        return Vector3.zero;
    }

    void Start()
    {
        trail = gameObject.GetComponentInChildren<TrailRenderer>();
        rightEar = GameObject.Find("ear right");
        leftEar = GameObject.Find("ear left");
        neck = GameObject.Find("neck");
        nose = GameObject.Find("nose 2");
        receiver = GameObject.Find("Mateuszek").GetComponent<Misbehaviour>();
		trail.enabled = false;
        cdnCounter = 0;
    }

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }

    void Update()
    {
		if (!fix)
			fix = true;
		else
			trail.enabled = true;
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
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
                    prevCastpoint = castpoint;
                    castpoint = new Vector3(Camera.main.ScreenPointToRay(mouse).origin.x, Camera.main.ScreenPointToRay(mouse).origin.y, 0);
                    transform.position = castpoint;
                }
            }
        }
        else
        {
            prevCastpoint = castpoint;
            castpoint = new Vector3(Camera.main.ScreenPointToRay(Input.mousePosition).origin.x, Camera.main.ScreenPointToRay(Input.mousePosition).origin.y, 0);
            transform.position = castpoint;

        }

		if (!enabled)
			return;

        if (prevCastpoint == Vector3.zero)
            return;

        if ((Application.platform == RuntimePlatform.Android) && (Input.touchCount <= 0))
            return;

        foreach (var hair in GameObject.FindGameObjectsWithTag("HairSegment"))
        {
            Vector3 point = Track.ProjectPointOnLineSegment(prevCastpoint, castpoint, hair.transform.position);
            if (Vector3.Distance(point, hair.transform.position) < cutThreeshold)
            {
                hair.GetComponent<HairSegment>().GetCut();
            }
        }

		Vector2 castpoint2D = new Vector2 (castpoint.x, castpoint.y);
		Vector2 step = new Vector2 (((castpoint - prevCastpoint) / precision).x, ((castpoint - prevCastpoint) / precision).y);
		if (cdnCounter >= cooldown) {
			for (int i = 0; i < precision; i++) {
				if (rightEar.GetComponent<CapsuleCollider2D> ().OverlapPoint (castpoint2D + step * (i + 1))) {
					receiver.cutRight = true;
					cdnCounter = 0f;
					receiver.GetComponent<AudioSource> ().Play ();
					break;
				}
				if (leftEar.GetComponent<CapsuleCollider2D> ().OverlapPoint (castpoint2D + step * (i + 1))) {
					receiver.cutLeft = true;
					cdnCounter = 0f;
					receiver.GetComponent<Quacker> ().Cry ();
					break;
				}
				if (nose.GetComponent<CapsuleCollider2D> ().OverlapPoint (castpoint2D + step * (i + 1))) {
					receiver.receive = true;
					cdnCounter = 0f;
					receiver.GetComponent<Quacker> ().Cry ();
					break;
				}
				if (neck.GetComponent<BoxCollider2D> ().OverlapPoint (castpoint2D + step * (i + 1))) {
					receiver.receive = true;
					cdnCounter = 0f;
					receiver.GetComponent<Quacker> ().Cry ();
					break;
				}
			}
		} else {
			cdnCounter += Time.deltaTime;
		}
    }
}
