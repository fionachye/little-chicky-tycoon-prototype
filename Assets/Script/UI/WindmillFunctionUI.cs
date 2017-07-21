using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindmillFunctionUI : MonoBehaviour
{

    public Sprite feedC,
        feedB,
        feedA,
        feedS,
        grainC,
        grainB,
        grainA,
        grainS,
        slotEmpty;

    GameObject grainImage,
        grainText,
        feedImage,
        feedText,
        cancelBtn,
        timeRem;

    void Start()
    {
        //initialize gameobject
        grainImage = transform.Find("CraftInput/Image").gameObject;
        grainText = transform.Find("CraftInput/Image/Text").gameObject;
        feedImage = transform.Find("CraftOutput/Image").gameObject;
        feedText = transform.Find("CraftOutput/Image/Text").gameObject;
        cancelBtn = transform.Find("CraftTime/CancelCraft").gameObject;
        timeRem = transform.Find("CraftTime/GearImg/Text").gameObject;
    }

    void Update()
    {
        //get all data to be displayed
    }

    void GetItemStat()
    {

    }

    void SetIcon()
    {

    }

    public void CancelCraft()
    {

    }

    void GetUpgradeReq()
    {

    }

    public void UpgradeWindmill()
    {

    }

    void GetCraftTime()
    {

    }
}
