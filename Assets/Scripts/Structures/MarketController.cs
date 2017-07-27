using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketController : MonoBehaviour {

    public static MarketController instance;
    public int CurrentLevel
    {
        get
        {
            return marketUpgradeManager.currentLevel;
        }

        set
        {
            this.currentLevel = value;
        }
    }

    MarketUpgradeManager marketUpgradeManager;
    const int CHICKEN_SOLD_START_AGE = 5; // chicken bought from market starts from age 5
    const int ITEM_COUNT = 4; // 4 different items sold on market
    const int INTERVAL = 15;  // sell different items every 15 minutes
    int currentLevel;
    int remainingTimeBeforeRefresh;
    float 
        gradeAProbability, 
        gradeBProbability, 
        gradeCProbability, 
        gradeSProbability;

    ChickenOnMarket currentChickenSold;
    EggOnMarket currentEggSold;
    GrainOnMarket currentGrainSold;
    FeedOnMarket currentFeedSold;
    DateTime lastSimulationTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        marketUpgradeManager = GetComponent<MarketUpgradeManager>();
    }

    private void Update()
    {
        GenerateMarketListing();
    }

    // market is unlocked from the start of the game
    bool Setup()
    {
        marketUpgradeManager.SetupMarket();
        currentLevel = marketUpgradeManager.currentLevel;

        // sell lowest grade item from the start
        if (currentLevel == 1)
        {
            currentChickenSold = new ChickenOnMarket(Chickens.Grade_C);
            currentEggSold = new EggOnMarket(Eggs.Grade_C);
            currentGrainSold = new GrainOnMarket(Grains.Grade_C);
            currentFeedSold = new FeedOnMarket(Feeds.Grade_C);
        }

        // TODO: load current sold item from save file


        return true;
    }

    bool Upgrade()
    {
        int upgradeCost = marketUpgradeManager.CalculateUpgradeCost();

        if (!WalletManager.instance.CanAfford(upgradeCost, Currency.Coin))
        {
            return false;
        }

        // only deduct coins when upgrade is successful
        if (marketUpgradeManager.UpgradeMarket())
        {
            WalletManager.instance.DeductMoney(upgradeCost, Currency.Coin);
            SetProbability();
            return true;
        }
        return false;
    }

    void GenerateMarketListing()
    {
        // check back every minute
        if (DateTime.UtcNow.Minute < lastSimulationTime.Minute)
            return;

        if (remainingTimeBeforeRefresh <= 0)
        {
            // generate items to sell
            GenerateRandomItemToSell();

            // reset timer
            remainingTimeBeforeRefresh = INTERVAL;
        }

        else
        {
            remainingTimeBeforeRefresh--;
        }
    }
    void GenerateRandomItemToSell()
    {
        float rand;

        // unlocks grade C only
        if (currentLevel == 1)
        {
            currentChickenSold = new ChickenOnMarket(Chickens.Grade_C);
            currentEggSold = new EggOnMarket(Eggs.Grade_C);
            currentFeedSold = new FeedOnMarket(Feeds.Grade_C);
            currentGrainSold = new GrainOnMarket(Grains.Grade_C);
        }

        // unlocks grade B and C
        // 70% Grade C, 30% Grade B
        if (currentLevel == 2)
        {
            // chicken
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeCProbability)
                currentChickenSold = new ChickenOnMarket(Chickens.Grade_C);
            else
                currentChickenSold = new ChickenOnMarket(Chickens.Grade_B);

            // egg
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeCProbability)
                currentEggSold = new EggOnMarket(Eggs.Grade_C);
            else
                currentEggSold = new EggOnMarket(Eggs.Grade_B);

            // grain
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeCProbability)
                currentGrainSold = new GrainOnMarket(Grains.Grade_C);
            else
                currentGrainSold = new GrainOnMarket(Grains.Grade_B);

            // feed
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeCProbability)
                currentFeedSold = new FeedOnMarket(Feeds.Grade_C);
            else
                currentFeedSold = new FeedOnMarket(Feeds.Grade_B);

        }

        // unlocks A, B and C
        // 10 % Grade C, 60 % Grade B, 30 % Grade A
        if (currentLevel == 3)
        {
            // chicken
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeBProbability)
                currentChickenSold = new ChickenOnMarket(Chickens.Grade_B);
            else if (rand >= gradeAProbability && rand < gradeBProbability)
                currentChickenSold= new ChickenOnMarket(Chickens.Grade_A);
            else
                currentChickenSold = new ChickenOnMarket(Chickens.Grade_C);

            // egg
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeBProbability)
                currentEggSold = new EggOnMarket(Eggs.Grade_B);
            else if (rand >= gradeAProbability && rand < gradeBProbability)
                currentEggSold = new EggOnMarket(Eggs.Grade_A);
            else
                currentEggSold = new EggOnMarket(Eggs.Grade_C);

            // grain
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeBProbability)
                currentGrainSold = new GrainOnMarket(Grains.Grade_B);
            else if (rand >= gradeAProbability && rand < gradeBProbability)
                currentGrainSold = new GrainOnMarket(Grains.Grade_A);
            else
                currentGrainSold = new GrainOnMarket(Grains.Grade_C);
        }

        // unlocks grade A, B, C and S
        // 10% Grade C, 20% Grade B, 40% Grade A, 30% Grade S
        if (currentLevel == 4)
        {
            // chicken
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeAProbability)
                currentChickenSold = new ChickenOnMarket(Chickens.Grade_A);
            else if (rand >= gradeSProbability && rand < gradeAProbability)
                currentChickenSold = new ChickenOnMarket(Chickens.Grade_S);
            else if (rand >= gradeBProbability && rand < gradeSProbability)
                currentChickenSold = new ChickenOnMarket(Chickens.Grade_B);
            else
                currentChickenSold = new ChickenOnMarket(Chickens.Grade_C);

            // egg
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeAProbability)
                currentEggSold = new EggOnMarket(Eggs.Grade_A);
            else if (rand >= gradeSProbability && rand < gradeAProbability)
                currentEggSold = new EggOnMarket(Eggs.Grade_S);
            else if (rand >= gradeBProbability && rand < gradeSProbability)
                currentEggSold = new EggOnMarket(Eggs.Grade_B);
            else
                currentEggSold = new EggOnMarket(Eggs.Grade_C);

            // grain
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeAProbability)
                currentGrainSold = new GrainOnMarket(Grains.Grade_A);
            else if (rand >= gradeSProbability && rand < gradeAProbability)
                currentGrainSold = new GrainOnMarket(Grains.Grade_S);
            else if (rand >= gradeBProbability && rand < gradeSProbability)
                currentGrainSold = new GrainOnMarket(Grains.Grade_B);
            else
                currentGrainSold = new GrainOnMarket(Grains.Grade_C);

            // feed
            rand = UnityEngine.Random.Range(0f, 1.0f);
            if (rand >= gradeAProbability)
                currentFeedSold = new FeedOnMarket(Feeds.Grade_A);
            else if (rand >= gradeSProbability && rand < gradeAProbability)
                currentFeedSold = new FeedOnMarket(Feeds.Grade_S);
            else if (rand >= gradeBProbability && rand < gradeSProbability)
                currentFeedSold = new FeedOnMarket(Feeds.Grade_B);
            else
                currentFeedSold = new FeedOnMarket(Feeds.Grade_C);
        }
    }

    void GenerateRandomItemToSellOffline()
    {

    }

    public void RemoveItem(CurrentItemOnMarket item)
    {
        switch(item)
        {
            case CurrentItemOnMarket.Chicken:
                currentChickenSold = null;
                break;
            case CurrentItemOnMarket.Egg:
                currentEggSold = null;
                break;
            case CurrentItemOnMarket.Feed:
                currentFeedSold = null;
                break;
            case CurrentItemOnMarket.Grain:
                currentGrainSold = null;
                break;
        }
    }

    // called when upgrade is done
    void SetProbability()
    {
        if (currentLevel == 1)
        {
            gradeAProbability = 0f;
            gradeBProbability = 0f;
            gradeCProbability = 1.0f;
            gradeSProbability = 0f;
        }
        if (currentLevel == 2)
        {
            gradeAProbability = 0f;
            gradeBProbability = 0.7f;
            gradeCProbability = 0.3f;
            gradeSProbability = 0f;
        }
        if (currentLevel == 3)
        {
            gradeAProbability = 0.3f;
            gradeBProbability = 0.6f;
            gradeCProbability = 0.10f;
            gradeSProbability = 0f;
        }
        if (currentLevel == 4)
        {
            gradeAProbability = 0.4f;
            gradeBProbability = 0.2f;
            gradeCProbability = 0.10f;
            gradeSProbability = 0.3f;
        }

    }
}
