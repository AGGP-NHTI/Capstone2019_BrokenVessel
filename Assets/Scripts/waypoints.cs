using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoints : MonoBehaviour {

    public GameObject[] listpoints;
    int current = 0;
    public float rotationSpeed = 5.0f;
    public float speed;
    public float WPradius = 1.0f;
    public bool rotatable = true;
    public bool rotateInPlace = false;
    public bool travel = true;
    public bool rotateInArc = false;
	public float rotationArc = 0.0f;
	public bool rotateOnX = false;
	public bool rotateOnY = false;
	public bool rotateOnZ = false;

    public GameObject parent;
    public bool moveWithParent = false;

    public bool moveUpDown = false;
    float moveSpeed = 0.005f;
    //Vector3 offset;
    Vector3 start;
    Vector3 end;
    Vector3 moveTo;
    

    // Use this for initialization
    void Start () {
       // offset = Vector3.down;
        start = transform.position;
        //end = transform.position + offset;
        end = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
    }


    // Update is called once per frame
    void Update() {

        if (moveWithParent)
        { 
        transform.rotation = Quaternion.Lerp(transform.rotation, parent.transform.rotation, (rotationSpeed * Time.deltaTime));
        }
        if (travel)
        {
            if (Vector3.Distance(listpoints[current].transform.position, transform.position) < WPradius)
            {
                current++;
                if (current >= listpoints.Length)
                {
                    current = 0;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, listpoints[current].transform.position, Time.deltaTime * speed);
            }
        }

        if(rotatable)
        { 
        Quaternion targetRotation = Quaternion.LookRotation(listpoints[current].transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Mathf.Min(rotationSpeed * Time.deltaTime, 1));
        }

        if (rotateInArc)
        {

            if (rotateOnX)
            {
                transform.rotation = Quaternion.Euler( rotationArc * Mathf.Sin(Time.time * rotationSpeed/360), 0f, 0f);
            }
            if (rotateOnY)
            {
                transform.rotation = Quaternion.Euler( 0f, rotationArc * Mathf.Sin(Time.time * rotationSpeed/360), 0f);
            }
            if (rotateOnZ)
		    {
		    transform.rotation = Quaternion.Euler(0f, 0f, rotationArc * Mathf.Sin(Time.time * rotationSpeed/360));
		    }
        }
        if(rotateInPlace)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            //transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
            //transform.Rotate(0, 5, 0, Space.World);
            //GetComponent<Rigidbody>().transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if(moveUpDown)
        {

            if (transform.position == start)
            {
                moveTo = end;
            }
            if (transform.position == end)
            {
                moveTo = start;
            }

            transform.position = Vector3.MoveTowards(transform.position, moveTo, moveSpeed);

        }


    }
}
