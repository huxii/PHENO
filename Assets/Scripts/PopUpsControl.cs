using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopUpsControl : MonoBehaviour
{
    public List<GameObject> popUpList;

    int curPop = 0;
    bool popping = false;
    bool popped = false;

	// Use this for initialization
	void Start ()
    { 
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!popping && curPop < popUpList.Count)
        {
            popping = true;
            popUpList[curPop].transform.localScale = new Vector3(0, 0, 0);
            popUpList[curPop].SetActive(true);
            popUpList[curPop].transform.DOScale(new Vector3(1, 1, 1), 0.5f)
                .OnComplete(() => { popped = true; });
            
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (popped)
            {
                popped = false;

                popUpList[curPop].transform.DOScale(new Vector3(0, 0, 0), 0.5f)
                    .OnComplete(() => { popUpList[curPop].SetActive(false); popping = false; });

                ++curPop;
                if (curPop >= popUpList.Count)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<MainControl>().StartGame();
                    Destroy(gameObject);
                }
            }
        }
	}
}
