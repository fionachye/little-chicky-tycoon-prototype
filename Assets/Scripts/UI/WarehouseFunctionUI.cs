using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarehouseFunctionUI : MonoBehaviour
{

    public Sprite feedC,
        feedB,
        feedA,
        feedS,
        grainC,
        grainB,
        grainA,
        grainS,
        eggC,
        eggB,
        eggA,
        eggS,
        slotEmpty,
        slotLock;

    List<GameObject> warehouseSlot,
        objectName,
        objectGrade,
        objectIcon,
        objectSell;

    void Start()
    {
        //check warehouse level to determine locked slots
        //initialize gameobject
        warehouseSlot = new List<GameObject>();
        objectName = new List<GameObject>();
        objectGrade = new List<GameObject>();
        objectIcon = new List<GameObject>();
        objectSell = new List<GameObject>();

        foreach (Transform child in transform)
        {
            warehouseSlot.Add(child.gameObject);
            objectName.Add(child.transform.Find("Image/Text").gameObject);
            objectIcon.Add(child.transform.Find("Image").gameObject);
            objectSell.Add(child.transform.Find("SellItem").gameObject);
        }

        objectName[2].GetComponent<Text>().text = "lanpi";
    }

    void Update()
    {
        //get all data to be displayed
    }

    void GetWarehouseLevel()
    {

    }

    void GetObjectStat()
    {

    }

    void SetIcon()
    {

    }

    public void SellObject()
    {

    }

    void GetUpgradeReq()
    {

    }

    public void UpgradeWarehouse()
    {

    }
}
