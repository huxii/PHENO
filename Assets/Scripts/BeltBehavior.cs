using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeltBehavior : MonoBehaviour
{
    public float speed = 3f;

    float w;
    float h;
    RectTransform rect;

	// Use this for initialization
	void Start ()
    {
        rect = GetComponent<RectTransform>();
        //w = Screen.width;
        //h = Screen.height;
        w = 800f;
        h = 600f;

        Vector3 pos = rect.localPosition;
        pos.y = 0;
        rect.localPosition = pos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = rect.localPosition;
        pos.y += speed * Time.deltaTime;

        if (speed < 0)
        {
            if (pos.y <= -h)
            {
                pos.y = 0;
            }
        }
        else
        {
            if (pos.y >= h)
            {
                pos.y = 0;
            }
        }

        rect.localPosition = pos;
	}
}
