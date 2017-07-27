using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillController : MonoBehaviour {

    public static WindmillController instance;
    public bool isBuilt { get; private set; }
    public int CurrentLevel
    {
        get
        {
            return windmillUpgradeManager.currentLevel;
        }

        set
        {
            this.currentLevel = value;
        }
    }
    public int CurrentCapacity
    {
        get
        {
            return windmillUpgradeManager.currentCapacity;
        }

        set
        {
            this.currentCapacity = value;
        }
    }

    int currentLevel, currentCapacity;
    WindmillUpgradeManager windmillUpgradeManager;

    const int CRAFT_TIME = 60; // 1 hour production time
    float craftRemainingTime;

    List<Grain> grains;
    DateTime lastSimulationTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        Craft();
    }

    // windmill is not unlocked from the start of the game
    bool Setup()
    {
        windmillUpgradeManager.SetupWindmill();
        currentCapacity = windmillUpgradeManager.currentCapacity;
        currentLevel = windmillUpgradeManager.currentLevel;
        isBuilt = true;
        // TODO: load chicken list from save file

        return true;
    }

    bool Upgrade()
    {
        int upgradeCost = windmillUpgradeManager.CalculateUpgradeCost();

        if (!WalletManager.instance.CanAfford(upgradeCost, Currency.Coin))
        {
            return false;
        }

        if (windmillUpgradeManager.UpgradeWindmill())
        {
            WalletManager.instance.DeductMoney(upgradeCost, Currency.Coin);
            return true;
        }
        return false;
    }

    void Craft()
    {
        // check back every minute
        if (DateTime.UtcNow.Minute < lastSimulationTime.Minute)
            return;

        if (grains == null || grains.Count == 0)
            return;

        if (craftRemainingTime <= 0)
        {
            OnCraftComplete();
        }
        else
        {
            craftRemainingTime--;
        }
    }

    void CraftOffline()
    {

    }

    void OnCraftComplete()
    {
        Grains grainGrade = grains[0].grade;
        Feeds feedGrade = Feeds.Grade_A;

        switch(grainGrade)
        {
            case Grains.Grade_A:
                feedGrade = Feeds.Grade_A;
                break;
            case Grains.Grade_B:
                feedGrade = Feeds.Grade_B;
                break;
            case Grains.Grade_C:
                feedGrade = Feeds.Grade_C;
                break;
            case Grains.Grade_S:
                feedGrade = Feeds.Grade_S;
                break;
        }

        // remove all grains from storage
        grains = null;

        // move products to warehouse
        Feed newFeed = new Feed();
        newFeed.Initialize(feedGrade);
        WarehouseController.instance.StoreFeed(newFeed);
        
        // reset craftRemainingTime
        craftRemainingTime = CRAFT_TIME;
    }   

    // import grains from warehouse
    public void AddGrainsFromWarehouse(Grains grade)
    {
        int retrieveCount = currentCapacity;
        grains = WarehouseController.instance.RetrieveGrains(grade, retrieveCount);
    }

    public int GetCraftRemainingTime()
    {
        return Mathf.RoundToInt(craftRemainingTime);
    }
}
