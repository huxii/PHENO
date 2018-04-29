using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltsControl : MonoBehaviour
{
    public List<GameObject> puzzlePiecePrefabs;

    Queue<GameObject> pieceUpQueue = new Queue<GameObject>();
    Queue<GameObject> pieceDownQueue = new Queue<GameObject>();

    int piecesNum;
    [SerializeField]
    float pieceUpTimer = 0;
    [SerializeField]
    float pieceDownTimer = 0;
    float pieceCD = 2f;

    // Use this for initialization
    void Start()
    {
        piecesNum = puzzlePiecePrefabs.Count;
        for (int i = 0; i < piecesNum; ++i)
        {
            GameObject newPiece = Instantiate(puzzlePiecePrefabs[i], transform);
            pieceUpQueue.Enqueue(newPiece);
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if (pieceUpTimer <= 0)
        {         
            if (pieceUpQueue.Count > 0)
            {
                pieceUpTimer = pieceCD;
                GameObject piece = pieceUpQueue.Dequeue();
                piece.GetComponent<PieceControl>().isMoving = true;
            }
        }
        else
        {
            pieceUpTimer -= Time.deltaTime;
        }

        if (pieceDownTimer <= 0)
        {
            if (pieceDownQueue.Count > 0)
            {
                pieceDownTimer = pieceCD;
                GameObject piece = pieceDownQueue.Dequeue();
                piece.GetComponent<PieceControl>().isMoving = true;
            }
        }
        else
        {
            pieceDownTimer -= Time.deltaTime;
        }
    }

    public void InUpQueue(GameObject piece)
    {
        pieceUpQueue.Enqueue(piece);
    }

    public void InDownQueue(GameObject piece)
    {
        pieceDownQueue.Enqueue(piece);
    }
}
