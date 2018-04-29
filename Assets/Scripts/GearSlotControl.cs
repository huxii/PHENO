using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GearSlotControl : MonoBehaviour
{
    public int id = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PieceHover(int num)
    {
        transform.parent.gameObject.GetComponent<GearControl>().friendGear.GetComponent<GearControl>().PieceHover((id + 10 - 3) % 20, num);
    }

    public void PieceHoverOver(int num)
    {
        transform.parent.gameObject.GetComponent<GearControl>().friendGear.GetComponent<GearControl>().PieceHoverOver((id + 10 - 3) % 20, num);
    }

    public bool IsPieceFit(GameObject obj)
    {
        return transform.parent.gameObject.GetComponent<GearControl>().IsFit(id, obj);
    }

    public void HidePiece()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ShowPiece()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Hover()
    {
        transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
    }

    public void HoverOver()
    {
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
    }
}
