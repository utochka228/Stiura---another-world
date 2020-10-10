using UnityEngine;
using System.Collections;

public class EquipmentInfo : MonoBehaviour {
    //////////////////////////////
    public Transform LeftHandSlot;
    public bool LeftHandIsEmpty;
    public string LeftHandItemIs;

    //////////////////////////////
    public Transform RightHandSlot;
    public bool RightHandIsEmpty;
    public string RightHandItemIs;

    //////////////////////////////
    public Transform Chest;
    public bool ChestIsEmpty;
    public string ChestItemIs;

    //////////////////////////////
    public Transform Head;
    public bool HeadIsEmpty;
    public string HeadItemIs;

    //////////////////////////////
    public Transform Pants;
    public bool PantsIsEmpty;
    public string PantsItemIs;

    //////////////////////////////
    public Transform Shoes;
    public bool ShoesIsEmpty;
    public string ShoesItemIs;

    void Update()
    {
        if (LeftHandSlot.childCount <= 0)
        {
            LeftHandIsEmpty = true;
            LeftHandItemIs = "";
        }
        if (LeftHandSlot.childCount > 0)
        {
            LeftHandIsEmpty = false;
        }
        //////
        if (RightHandSlot.childCount <= 0)
        {
            RightHandIsEmpty = true;
            RightHandItemIs = "";
        }
        if (RightHandSlot.childCount > 0)
        {
            RightHandIsEmpty = false;
        }
        //////
        if (Chest.childCount <= 0)
        {
            ChestIsEmpty = true;
        }
        if (Chest.childCount > 0)
        {
            ChestIsEmpty = false;
        }
        //////
        if (Head.childCount <= 0)
        {
            HeadIsEmpty = true;
        }
        if (Head.childCount > 0)
        {
            HeadIsEmpty = false;
        }
        //////
        if (Pants.childCount <= 0)
        {
            PantsIsEmpty = true;
        }
        if (Pants.childCount > 0)
        {
            PantsIsEmpty = false;
        }
        //////
        if (Shoes.childCount <= 0)
        {
            ShoesIsEmpty = true;
        }
        if (Shoes.childCount > 0)
        {
            ShoesIsEmpty = false;
        }
        //////
        if (LeftHandIsEmpty == false)
            LeftHandItemIs = LeftHandSlot.GetChild(0).GetComponent<InstrumentHolder>().instItem.itemTag;

        if (RightHandIsEmpty == false)
            RightHandItemIs = RightHandSlot.GetChild(0).GetComponent<InstrumentHolder>().instItem.itemTag;
    }
}
