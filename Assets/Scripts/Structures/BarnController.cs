using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BarnController : MonoBehaviour {

    public static BarnController instance;
    public int ChickenCount
    {
        get
        {
            return chickenList.Count;
        }
    }
    public int CurrentLevel
    {
        get
        {
            return barnUpgradeManager.currentLevel;
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
            return barnUpgradeManager.currentCapacity;
        }

        set
        {
            this.currentCapacity = value;
        }
    }

    int currentLevel, currentCapacity;
    List<Chicken> chickenList;
    BarnUpgradeManager barnUpgradeManager;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        barnUpgradeManager = GetComponent<BarnUpgradeManager>();
        chickenList = new List<Chicken>();
    }

    // barn is unlocked from the start of the game
    bool Setup()
    {
        barnUpgradeManager.SetupBarn();
        currentCapacity = barnUpgradeManager.currentCapacity;
        currentLevel = barnUpgradeManager.currentLevel;
        // TODO: load chicken list from save file

        return true;
    }

    bool Upgrade()
    {
        int upgradeCost = barnUpgradeManager.CalculateUpgradeCost();

        if (!WalletManager.instance.CanAfford(upgradeCost, Currency.Coin))
            return false;

        if (barnUpgradeManager.UpgradeBarn())
        {
            WalletManager.instance.DeductMoney(upgradeCost, Currency.Coin);
            return true;
        }

        return false;
    }

    public void SellChicken(Chicken chicken)
    {
        Chicken soldChicken = null;
        bool success = false;

        foreach(Chicken c in chickenList)
        {
            if (c.id == chicken.id)
            {
                soldChicken = c;
                chickenList.Remove(c);
                break;
            }
        }

        success = ChickenManager.instance.RemoveChicken(soldChicken.id, soldChicken.grade);
        Assert.IsTrue(success);
        WalletManager.instance.AddMoney(soldChicken.marketValue, Currency.Coin);
    }

    public void AddChicken(Chicken chicken)
    {
        chickenList.Add(chicken);
    }

    public bool IsAtFullCapacity()
    {
        Assert.IsFalse(ChickenCount > currentCapacity);
        return ChickenCount == currentCapacity;
    }
}
