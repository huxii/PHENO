using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceControl : MonoBehaviour
{
    public bool isMoving = false;
    public List<GameObject> pieceLetters;
    public float speed = 3f;
    public float letterRange = 1.2f;

    bool isGoingUp = true;
    float heightRange = 8f;
    float track1X = 4.45f;
    float track2X = 6.8f;
    float modelYMakeup = -0.15f;
    float localZOffset = -0.1f;

    // Use this for initialization
    void Start ()
    {
        transform.localPosition = new Vector3(track1X, -heightRange, 0);

        GenerateLetters();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MovingOnBelt();
    }

    void MovingOnBelt()
    {
        if (isMoving)
        {
            Vector3 pos = transform.localPosition;

            if (isGoingUp)
            {
                pos.y += Time.deltaTime * speed;
                if (pos.y >= heightRange)
                {
                    isGoingUp = false;
                    pos.x = track2X;
                    isMoving = false;
                    transform.parent.GetComponent<BeltsControl>().InDownQueue(gameObject);
                }
            }
            else
            {
                pos.y -= Time.deltaTime * speed;
                if (pos.y <= -heightRange)
                {
                    isGoingUp = true;
                    pos.x = track1X;
                    isMoving = false;
                    transform.parent.GetComponent<BeltsControl>().InUpQueue(gameObject);
                }
            }

            transform.localPosition = pos;
        }
    }

    void GenerateLetters()
    {
        if (pieceLetters.Count == 1)
        {
            GameObject obj = Instantiate(pieceLetters[0], transform);
            obj.transform.localPosition = new Vector3(0, modelYMakeup, localZOffset);
        }
        else
        {
            float letterInterval = letterRange / (pieceLetters.Count - 1);
            Vector3 pos = new Vector3(0, -letterRange / 2 + modelYMakeup, localZOffset);
            for (int i = 0; i < pieceLetters.Count; ++i)
            {
                GameObject obj = Instantiate(pieceLetters[i], transform);
                obj.transform.localPosition = pos;
                pos.y += letterInterval;
            }
        }
    }
}
