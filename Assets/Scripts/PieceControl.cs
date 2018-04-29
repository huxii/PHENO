using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PieceControl : MonoBehaviour
{
    public bool isMoving = false;
    public List<MainControl.PieceType> pieceLetterTypes;
    public List<GameObject> pieceLetterPrefabs;
    public float speed = 3f;
    public float letterRange = 1.2f;

    bool isGoingUp = true;
    float heightRange = 8f;
    float track1X = 4.45f;
    float track2X = 6.8f;
    float localZOffset = -0.1f;
    bool isAttaching = false;
    Vector3 startPos;
    GameObject targetObj;

    // Use this for initialization
    void Start ()
    {
        startPos = new Vector3(track1X, -heightRange, 0);
        transform.localPosition = startPos;

        GenerateLetters();
	}

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MovingOnBelt();
        }
        else
        {
            if (isAttaching)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = pos.z = localZOffset - 1.5f;
                transform.localPosition = pos;
            }
        }
    }

    void MovingOnBelt()
    {
        Vector3 pos = transform.localPosition;

        if (isGoingUp)
        {
            pos.x = track1X;
            pos.y += Time.deltaTime * speed;
            pos.z = localZOffset;
            if (pos.y >= heightRange)
            {
                isMoving = false;
                ReadyGoingDown();
            }
        }
        else
        {
            pos.x = track2X;
            pos.y -= Time.deltaTime * speed;
            pos.z = localZOffset;
            if (pos.y <= -heightRange)
            {
                isMoving = false;
                ReadyGoingUp();
            }
        }

        transform.localPosition = pos;
    }

    void GenerateLetters()
    {
        if (pieceLetterTypes.Count == 1)
        {
            GameObject obj = Instantiate(pieceLetterPrefabs[(int)pieceLetterTypes[0]], transform);
            obj.transform.localPosition = new Vector3(0, 0, localZOffset);
        }
        else
        {
            float letterInterval = letterRange / (pieceLetterTypes.Count - 1);
            Vector3 pos = new Vector3(0, -letterRange / 2, localZOffset);
            for (int i = 0; i < pieceLetterTypes.Count; ++i)
            {
                GameObject obj = Instantiate(pieceLetterPrefabs[(int)pieceLetterTypes[i]], transform);
                obj.transform.localPosition = pos;
                pos.y += letterInterval;
            }
        }
    }

    void OnMouseDown()
    {
        isAttaching = true;
        isMoving = false;
    }

    void OnMouseUp()
    {
        isAttaching = false;

        bool isFit = targetObj.GetComponent<GearSlotControl>().IsPieceFit(gameObject);
        if (isFit)
        {
            targetObj.GetComponent<GearSlotControl>().PieceHoverOver(pieceLetterTypes.Count);
            transform.DOScale(new Vector3(0, 0, 0), 0.2f).OnComplete(
                () => { Destroy(gameObject); });
        }
        else
        {
            targetObj.GetComponent<GearSlotControl>().PieceHoverOver(pieceLetterTypes.Count);
            transform.DOLocalMove(startPos, 0.2f).SetEase(Ease.InOutCubic)
                .OnComplete(() => { ReadyGoingUp(); });
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isAttaching)
        {
            if (other.gameObject.CompareTag("GearSlot"))
            {
                if (targetObj)
                {
                    targetObj.GetComponent<GearSlotControl>().PieceHoverOver(pieceLetterTypes.Count);
                    Debug.Log(targetObj.name);
                }
                targetObj = other.gameObject;
                targetObj.GetComponent<GearSlotControl>().PieceHover(pieceLetterTypes.Count);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isAttaching)
        {
            if (other.gameObject.CompareTag("GearSlot"))
            {
                other.gameObject.GetComponent<GearSlotControl>().PieceHoverOver(pieceLetterTypes.Count);
            }
        }
    }

    public void ReadyGoingUp()
    {
        isGoingUp = true;
        transform.parent.GetComponent<BeltsControl>().InUpQueue(gameObject);
    }

    public void ReadyGoingDown()
    {
        isGoingUp = false;
        transform.parent.GetComponent<BeltsControl>().InDownQueue(gameObject);
    }
}
