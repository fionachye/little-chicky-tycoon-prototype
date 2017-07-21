using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatcheryFunctionUI : MonoBehaviour
{

    public Sprite hatchC,
        hatchB,
        hatchA,
        hatchS,
        slotEmpty,
        slotLock;

    List<GameObject> eggSlot,
        hatchTime,
        eggGrade,
        eggIcon,
        hatchCancel;

    void Start()
    {
        //check hatchery level to determine locked slots
        //initialize gameobject
        eggSlot = new List<GameObject>();
        hatchTime = new List<GameObject>();
        eggGrade = new List<GameObject>();
        eggIcon = new List<GameObject>();
        hatchCancel = new List<GameObject>();

        foreach (Transform child in transform)
        {
            eggSlot.Add(child.gameObject);
            hatchTime.Add(child.transform.Find("Image/Text").gameObject);
            eggIcon.Add(child.transform.Find("Image").gameObject);
            hatchCancel.Add(child.transform.Find("CancelHatch").gameObject);
        }

        hatchTime[0].GetComponent<Text>().text = "lanpi";
    }

    void Update()
    {
        //get all data to be displayed
    }

    void GetHatcheryLevel()
    {

    }

    void GetEggStat()
    {

    }

    void SetIcon()
    {

    }

    public void CancelHatch()
    {

    }

    void GetUpgradeReq()
    {

    }

    public void UpgradeHatchery()
    {

    }
}
