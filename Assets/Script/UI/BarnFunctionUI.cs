using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarnFunctionUI : MonoBehaviour {

    public Sprite chickenC,
        chickenB,
        chickenA,
        chickenS,
        slotEmpty,
        slotLock;

    List<GameObject> barnSlot,
        chickenName,
        chickenGrade,
        chickenHunger,
        chickenAge,
        chickenIcon,
        chickenSell;

    void Start ()
    {
        //check barn level to determine locked slots
        //initialize gameobject
        barnSlot = new List<GameObject>();
        chickenName = new List<GameObject>();
        chickenGrade = new List<GameObject>();
        chickenHunger = new List<GameObject>();
        chickenAge = new List<GameObject>();
        chickenIcon = new List<GameObject>();
        chickenSell = new List<GameObject>();

        foreach (Transform child in transform)
        {
            barnSlot.Add(child.gameObject);
            chickenName.Add(child.transform.Find("ChangeChickenName/Text").gameObject);
            chickenIcon.Add(child.transform.Find("Image").gameObject);
            chickenSell.Add(child.transform.Find("SellChicken").gameObject);
        }

        chickenName[1].GetComponent<Text>().text = "lanpi";
	}
	
	void Update ()
    {
		//get all data to be displayed
	}

    void GetBarnLevel ()
    {

    }

    void GetChickenStat ()
    {

    }

    public void ChangeChickenName ()
    {

    }

    void SetIcon ()
    {

    }

    public void SellChicken ()
    {

    }

    void GetUpgradeReq ()
    {

    }

    public void UpgradeBarn ()
    {

    }
}
