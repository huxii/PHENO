using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GearControl : MonoBehaviour
{
    public float rotSpeed = 20f;
    public float rotStep = 10f;
    public GameObject friendGear;

    Vector3 mousePos;
    bool isPositive = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseDown()
    {
    }

    void OnMouseUp()
    {
        int step = (int)(transform.localEulerAngles.z / rotStep);
        float realSum = step * rotStep;
        float mod = Mathf.Abs(transform.localEulerAngles.z - realSum);

        if (isPositive)
        {
            if (mod > rotStep / 2)
            {
                ++step;
            }
            ++step;
        }
        else
        {
            if (mod > rotStep / 2)
            {
                --step;
            }
            --step;
        }

        float rotX = step * rotStep;
        if (rotX >= 180)
        {
            rotX -= 360;
        }
        else
        {
            if (rotX <= -180)
            {
                rotX += 360;
            }
        }

        transform.DORotate(new Vector3(0, 0, rotX), 0.2f);
        friendGear.transform.DORotate(new Vector3(0, 0, -rotX), 0.2f);
    }

    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed;
        isPositive = (rotX >= 0);

        transform.Rotate(Vector3.forward, rotX);
        friendGear.transform.Rotate(Vector3.forward, -rotX);
    }
}
