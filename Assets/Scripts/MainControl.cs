using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainControl : MonoBehaviour
{
    public enum PieceType
    {
        A,
        T,
        C,
        G
    };

    public GameObject gameScene;
    public GameObject endScene;

    [SerializeField]
    GameObject[] gearList;
    [SerializeField]
    GameObject[] pieceList;
    GameObject bar;
    GameObject buttons;
    bool isEnding = false;

	// Use this for initialization
	void Start ()
    {
        gearList = GameObject.FindGameObjectsWithTag("Gear");
        bar = GameObject.FindGameObjectWithTag("Bar");
        buttons = GameObject.FindGameObjectWithTag("Buttons");
        buttons.SetActive(false);

        gameScene.transform.position = new Vector3(-20f, 0, 0);
        gameScene.transform.DOMove(new Vector3(0, 0, 0), 1f).SetEase(Ease.InOutCubic);

        Vector3 pos = bar.transform.localPosition;
        bar.transform.position = new Vector3(20f, 0, 0);
        bar.transform.DOLocalMove(pos, 1f).SetEase(Ease.InOutCubic);
    }
	
	// Update is called once per frame
	void Update ()
    {
        pieceList = GameObject.FindGameObjectsWithTag("Piece");
    }
    
    public void EndGame()
    {
        if (!isEnding)
        {
            isEnding = true;

            foreach (GameObject piece in pieceList)
            {
                piece.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InCubic);
            }

            gearList[0].transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InCubic);
            gearList[1].transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InCubic).OnComplete(() => 
                {
                    bar.transform.DOMove(new Vector3(600, 0, 0), 0.5f).SetEase(Ease.InCubic);
                    buttons.transform.DOMove(new Vector3(0, 20f, 0), 0.5f).SetEase(Ease.InCubic).OnComplete(
                        () => 
                        {
                            gameScene.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() => { gameScene.SetActive(false); endScene.SetActive(true); });
                        });              
                });
            
        }
    }

    public void RestartGame()
    {
        if (!isEnding)
        {
            isEnding = true;

            foreach (GameObject piece in pieceList)
            {
                piece.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InCubic);
            }

            gearList[0].transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InCubic);
            gearList[1].transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
            {
                bar.transform.DOMove(new Vector3(600, 0, 0), 0.5f).SetEase(Ease.InCubic);
                buttons.transform.DOMove(new Vector3(0, 20f, 0), 0.5f).SetEase(Ease.InCubic).OnComplete(
                    () => { gameScene.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() => { ReloadGame(); }); }
                    );               
            });

        }
    }

    public void StartGame()
    {
        buttons.transform.position = new Vector3(0, 20f, 0);
        buttons.SetActive(true);
        buttons.transform.DOMove(new Vector3(1.3f, 4f, 0), 0.5f);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
