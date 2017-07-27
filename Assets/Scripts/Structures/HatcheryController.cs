using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatcheryController : MonoBehaviour {

    public static HatcheryController instance;
    public bool isBuilt { get; private set; }
    public int CurrentLevel
    {
        get
        {
            return hatcheryUpgradeManager.currentLevel;
        }

        set
        {
            currentLevel = value;
        }
    }

    public int IncubatorCount
    {
        get
        {
            return hatcheryUpgradeManager.incubatorCount;
        }

        set
        {
            incubatorCount = value;
        }
    }

    public int eggCount { get; private set; }
    public int hatchingCount { get; private set; }

    HatcheryUpgradeManager hatcheryUpgradeManager;

    int incubatorCount, currentLevel, currentUsedIncubatorCount;
    float probabilityRareEgg;
    int currentHatchingCount;
    

    // eggs stored in incubator
    List<Egg> eggs;

    // the last time simulation was run
    DateTime lastSimulationTime;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        
        hatcheryUpgradeManager = GetComponent<HatcheryUpgradeManager>();
        eggs = new List<Egg>();
    }

    void Update()
    {
        if (!isBuilt)
            return;

        HatchEggs();
    }

    // not unlocked at the start of the game
    public bool Setup()
    {
        if (!WalletManager.instance.CanAfford(HatcheryUpgradeManager.BUILD_COST, Currency.Coin))
            return false;

        hatcheryUpgradeManager.SetupHatchery();
        WalletManager.instance.DeductMoney(HatcheryUpgradeManager.BUILD_COST, Currency.Coin);
        isBuilt = true;
        return true;
    }

    bool Upgrade()
    {
        int upgradeCost = hatcheryUpgradeManager.CalculateUpgradeCost();

        if (!WalletManager.instance.CanAfford(upgradeCost, Currency.Coin))
            return false;

        if (hatcheryUpgradeManager.UpgradeHatchery())
        {
            WalletManager.instance.DeductMoney(upgradeCost, Currency.Coin);
            return true;
        }

        return false;
    }

    void HatchEggs()
    {
        // check back every minute
        if (DateTime.UtcNow.Minute < lastSimulationTime.Minute)
            return;

        // do not hatch eggs when barn is full
        if (BarnController.instance.IsAtFullCapacity())
            return;

        foreach(Egg e in eggs)
        {
            float remainingTime = e.hatchRemainingTime;
            if (remainingTime <= 0)
            {
                OnEggHatched(e);
            }
            else
            {
                // update hatch remaining time
                e.SetHatchRemainingTime(remainingTime - 1);
            }
        }
    }

    void HatchEggsOffline()
    {

    }

    void OnEggHatched(Egg egg)
    {
        Chickens grade = Chickens.Grade_A;

        switch(egg.grade)
        {
            case Eggs.Grade_A:
                grade = Chickens.Grade_A;
                break;
            case Eggs.Grade_B:
                grade = Chickens.Grade_B;
                break;
            case Eggs.Grade_C:
                grade = Chickens.Grade_C;
                break;
            case Eggs.Grade_S:
                grade = Chickens.Grade_S;
                break;
        }
        // transfer chicken
        ChickenManager.instance.InitializeChicken(grade);
        eggs.Remove(egg);
    }

    int GetHatchingEggCOunt()
    {
        return eggs.Count;
    }

    // called by WarehouseController when player choose to hatch an egg
    public bool HatchNewEgg(Egg egg)
    {
        // can hatch egg only when barn is not full
        if (BarnController.instance.IsAtFullCapacity())
            return false;

        // cant hatch egg if adding this new egg will exceed the current incubator count
        if (eggs.Count >= incubatorCount)
            return false;

        // add this new egg to current list
        eggs.Add(egg);
        egg.StartHatch();
        return true;
    }
}
