using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltsControl : MonoBehaviour
{
    public List<GameObject> puzzlePiecePrefabs;

    Queue<GameObject> pieceUpQueue = new Queue<GameObject>();
    Queue<GameObject> pieceDownQueue = new Queue<GameObject>();

    int piecesNum;
    float pieceUpTimer = 0;
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
        pieceUpTimer -= Time.deltaTime;
        pieceDownTimer -= Time.deltaTime;
        if (pieceUpTimer <= 0)
        {
            pieceUpTimer = pieceCD;
            if (pieceUpQueue.Count > 0)
            {
                GameObject piece = pieceUpQueue.Dequeue();
                piece.GetComponent<PieceControl>().isMoving = true;
            }
        }

        if (pieceDownTimer <= 0)
        {
            pieceDownTimer = pieceCD;
            if (pieceDownQueue.Count > 0)
            {
                GameObject piece = pieceDownQueue.Dequeue();
                piece.GetComponent<PieceControl>().isMoving = true;
            }
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
