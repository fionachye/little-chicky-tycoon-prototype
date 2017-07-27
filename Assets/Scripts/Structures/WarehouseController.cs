using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseController : MonoBehaviour {

    public static WarehouseController instance;
    public int CurrentLevel
    {
        get
        {
            return warehouseUpgradeManager.currentLevel;
        }

        set
        {
            this.currentLevel = value;
        }
    }
    public int CurrentStorageCapacity
    {
        get
        {
            return warehouseUpgradeManager.currentCapacity;
        }

        set
        {
            this.currentStorageCapacity = value;
        }
    }

    public int occupiedStorage { get; private set; }

    int 
        currentLevel,
        currentStorageCapacity;
    List<Egg> Eggs_GradeA, Eggs_GradeB, Eggs_GradeC, Eggs_GradeS;
    List<Grain> Grain_GradeA, Grain_GradeB, Grain_GradeC, Grain_GradeS;
    List<Feed> Feed_GradeA, Feed_GradeB, Feed_GradeC, Feed_GradeS;
    WarehouseUpgradeManager warehouseUpgradeManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        warehouseUpgradeManager = GetComponent<WarehouseUpgradeManager>();
        
        // initialize eggs
        Eggs_GradeA = new List<Egg>();
        Eggs_GradeB = new List<Egg>();
        Eggs_GradeC = new List<Egg>();
        Eggs_GradeS = new List<Egg>();

        // initialize grains
        Grain_GradeA = new List<Grain>();
        Grain_GradeB = new List<Grain>();
        Grain_GradeC = new List<Grain>();
        Grain_GradeS = new List<Grain>();

        // initialize feeds
        Feed_GradeA = new List<Feed>();
        Feed_GradeB = new List<Feed>();
        Feed_GradeC = new List<Feed>();
        Feed_GradeS = new List<Feed>();
    }

    // warehouse is unlocked from the start of the game
    bool Setup()
    {
        warehouseUpgradeManager.SetupWarehouse();
        currentStorageCapacity = warehouseUpgradeManager.currentCapacity;
        currentLevel = warehouseUpgradeManager.currentLevel;
        
        // TODO: load chicken list from save file

        return true;
    }

    bool Upgrade()
    {
        int upgradeCost = warehouseUpgradeManager.CalculateUpgradeCost();

        if (!WalletManager.instance.CanAfford(upgradeCost, Currency.Coin))
        {
            return false;
        }

        if (warehouseUpgradeManager.UpgradeWarehouse())
        {
            WalletManager.instance.DeductMoney(upgradeCost, Currency.Coin);
            return true;
        }

        return false;

    }

    public List<Feed> RetrieveChickenFeed(Feeds grade)
    {
        switch(grade)
        {
            case Feeds.Grade_A:
                return Feed_GradeA;
            case Feeds.Grade_B:
                return Feed_GradeB;
            case Feeds.Grade_C:
                return Feed_GradeC;
            case Feeds.Grade_S:
                return Feed_GradeS;
        }
        return null;
    }

    // retrieve a selection of eggs from warehouse
    public List<Egg> RetrieveEggs(Eggs grade)
    {
        switch(grade)
        {
            case Eggs.Grade_A:
                return Eggs_GradeA;
            case Eggs.Grade_B:
                return Eggs_GradeB;
            case Eggs.Grade_C:
                return Eggs_GradeC;
            case Eggs.Grade_S:
                return Eggs_GradeS;
        }
        return null;
    }

    // retrive a selection of grains from warehouse
    // called by WindmillController
    public List<Grain> RetrieveGrains(Grains grade, int count)
    {
        List<Grain> grains = null;
        List<Grain> retrievedGrains = new List<Grain>();
        int retrievedCount = 0;

        switch (grade)
        {
            case Grains.Grade_A:
                grains = Grain_GradeA;
                break;
            case Grains.Grade_B:
                grains = Grain_GradeB;
                break;
            case Grains.Grade_C:
                grains = Grain_GradeC;
                break;
            case Grains.Grade_S:
                grains = Grain_GradeS;
                break;
        }

        for(int i = 0; i < count && i < grains.Count; i++)
        {
            retrievedGrains.Add(grains[i]);
            retrievedCount++;
        }

        // update the current capacity
        currentStorageCapacity -= retrievedCount;

        return retrievedGrains;
    }

    public int GetChickenFeedCount(Feeds grade)
    {
        switch(grade)
        {
            case Feeds.Grade_A:
                return Feed_GradeA.Count;
            case Feeds.Grade_B:
                return Feed_GradeB.Count;
            case Feeds.Grade_C:
                return Feed_GradeC.Count;
            case Feeds.Grade_S:
                return Feed_GradeS.Count;

        }
        return -1;
    }

    public int GetEggCount(Eggs grade)
    {
        switch (grade)
        {
            case Eggs.Grade_A:
                return Eggs_GradeA.Count;
            case Eggs.Grade_B:
                return Eggs_GradeB.Count;
            case Eggs.Grade_C:
                return Eggs_GradeC.Count;
            case Eggs.Grade_S:
                return Eggs_GradeS.Count;
        }
        return -1;
    }

    public int GetGrainCount(Grains grade)
    {
        switch(grade)
        {
            case Grains.Grade_A:
                return Grain_GradeA.Count;
            case Grains.Grade_B:
                return Grain_GradeB.Count;
            case Grains.Grade_C:
                return Grain_GradeC.Count;
            case Grains.Grade_S:
                return Grain_GradeS.Count;
        }
        return -1;
    }

    // store single egg
    // called by hatchery
    public void StoreEgg(Egg egg)
    {
        switch(egg.grade)
        {
            case Eggs.Grade_A:
                Eggs_GradeA.Add(egg);
                break;
            case Eggs.Grade_B:
                Eggs_GradeB.Add(egg);
                break;
            case Eggs.Grade_C:
                Eggs_GradeC.Add(egg);
                break;
            case Eggs.Grade_S:
                Eggs_GradeS.Add(egg);
                break;
        }
        // update storage count
        occupiedStorage++;
    }

    // store single grain
    // called by market
    public void StoreGrain(Grain grain)
    {
        
    }

    // store single feed
    // called by windmill
    public void StoreFeed(Feed feed)
    {
        switch(feed.grade)
        {
            case Feeds.Grade_A:
                Feed_GradeA.Add(feed);
                break;
            case Feeds.Grade_B:
                Feed_GradeB.Add(feed);
                break;
            case Feeds.Grade_C:
                Feed_GradeC.Add(feed);
                break;
            case Feeds.Grade_S:
                Feed_GradeS.Add(feed);
                break;
        }
        // update storage count
        occupiedStorage++;
    }

    public bool IsStorageFull()
    {
        return occupiedStorage >= currentStorageCapacity;
    }

}
