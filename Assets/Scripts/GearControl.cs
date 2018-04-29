using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GearControl : MonoBehaviour
{
    public bool isContronable = true;
    public float rotSpeed = 20f;
    public float rotStep = 10f;
    public GameObject friendGear;
    public List<MainControl.PieceType> gearLetterTypes; 
    public List<GameObject> gearLetterPrefabs;
    public List<bool> isFixed;

    List<GameObject> gearLetterList;
    [SerializeField]
    List<bool> isTaken;

    Vector3 mousePos;
    bool isPositive = false;
    float initFriendRot = -18f;

	// Use this for initialization
	void Start ()
    {
        GenerateLetters();
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
        if (isContronable)
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
                    //--step;
                }
                //--step;
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

            transform.DORotate(new Vector3(0, 0, rotX), 0.2f).SetEase(Ease.InOutCubic);
            friendGear.transform.DORotate(new Vector3(0, 0, initFriendRot - rotX), 0.2f).SetEase(Ease.InOutCubic);
        }
    }

    void OnMouseDrag()
    {
        if (isContronable)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed;
            isPositive = (rotX >= 0);

            transform.Rotate(Vector3.forward, rotX);
            friendGear.transform.Rotate(Vector3.forward, -rotX);
        }
    }

    void GenerateLetters()
    {
        gearLetterList = new List<GameObject>();
        isTaken = new List<bool>(new bool [gearLetterTypes.Count]);
        for (int i = 0; i < gearLetterTypes.Count; ++i)
        {
            int type = (int)gearLetterTypes[i];
            GameObject newLetter = Instantiate(gearLetterPrefabs[type], transform);
            newLetter.transform.localEulerAngles = new Vector3(0, 0, i * rotStep / 2);
            newLetter.GetComponent<GearSlotControl>().id = i;
            
            if (isFixed[i])
            {
                isTaken[i] = true;
                Destroy(newLetter.GetComponent<BoxCollider>());
            }
            else
            {
                if (!isContronable)
                {
                    newLetter.GetComponentInChildren<GearSlotControl>().HidePiece();
                }
            }
            
            gearLetterList.Add(newLetter);
        }
    }

    public void PieceHover(int id, int num)
    {
        for (int i = 0; i < num; ++i)
        {
            int j = (id + i) % gearLetterList.Count;
            gearLetterList[j].GetComponent<GearSlotControl>().Hover();
        }
    }

    public void PieceHoverOver(int id, int num)
    {
        for (int i = 0; i < num; ++i)
        {
            int j = (id + i) % gearLetterList.Count;
            gearLetterList[j].GetComponent<GearSlotControl>().HoverOver();
        }
    }

    public bool IsFit(int id, GameObject obj)
    {
        //Debug.Log("...........start");
        PieceControl piece = obj.GetComponent<PieceControl>();
        int count = piece.pieceLetterTypes.Count;
        Debug.Log(count);
        for (int i = 0; i < count; ++i)
        {
            int j = (id + i) % gearLetterList.Count; Debug.Log(gearLetterTypes[j] + " " + piece.pieceLetterTypes[i]);
            if (isTaken[j] || piece.pieceLetterTypes[i] != gearLetterTypes[j])
            {
                return false;
            }
            
        }
        //Debug.Log("...........end");

        for (int i = 0; i < count; ++i)
        {
            int j = (id + i) % gearLetterList.Count;
            isTaken[j] = true;
            gearLetterList[j].GetComponentInChildren<GearSlotControl>().ShowPiece();
        }
        return true;
    }
}
