using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndControl : MonoBehaviour
{
    public GameObject DNA1;
    public GameObject DNA2;
    public GameObject reward;
    public GameObject menu;

    int stage = 0;

	// Use this for initialization
	void Start ()
    {
        DNA1.SetActive(true);
        DNA2.SetActive(false);
        reward.SetActive(false);
        menu.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            if (stage == 0)
            {
                DNA2.transform.localScale = new Vector3(0, 0, 0);
                DNA2.SetActive(true);
                DNA2.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() => { ++stage; });
                ++stage;
            }
            else
            if (stage == 2)
            {
                DNA1.transform.DOLocalMove(new Vector3(DNA1.transform.localPosition.x, -20, 0), 1f);
                DNA2.transform.DOLocalMove(new Vector3(DNA2.transform.localPosition.x, -20, 0), 1f);

                reward.transform.localPosition = new Vector3(0, 20, 0);
                reward.SetActive(true);
                reward.transform.DOLocalMove(new Vector3(0, 1, 0), 1f);

                Vector3 pos = menu.transform.localPosition;
                menu.transform.localPosition = new Vector3(0, 800, 0);
                menu.SetActive(true);
                menu.transform.DOLocalMove(pos, 1f);

                ++stage;
            }
        }
	}
}
