using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketFunctionUI : MonoBehaviour
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
        chickenC,
        chickenB,
        chickenA,
        chickenS;

    GameObject chickenIcon,
        feedIcon,
        eggIcon,
        grainIcon,
        marketReset,
        timeRem;

    void Start()
    {
        //check market level to determine reset ability
        //initialize gameobject
        chickenIcon = transform.Find("MarketChicken/Image").gameObject;
        feedIcon = transform.Find("MarketFeed/Image").gameObject;
        eggIcon = transform.Find("MarketEgg/Image").gameObject;
        grainIcon = transform.Find("MarketGrain/Image").gameObject;
        marketReset = transform.Find("MarketReset").gameObject;
        grainIcon = transform.Find("MarketReset/Image/Text").gameObject;
    }

    void Update()
    {
        //get all data to be displayed
    }

    void GetMarketLevel()
    {

    }

    void GetItemStat()
    {

    }

    void SetIcon()
    {

    }

    public void BuyItem()
    {

    }

    void GetUpgradeReq()
    {

    }

    public void UpgradeMarket()
    {

    }

    void GetResetTime()
    {

    }

    public void ResetMarketNow()
    {

    }
}
