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
    public int IncubatorCount
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

    void Setup()
    {
        barnUpgradeManager.SetupBarn();
        currentCapacity = barnUpgradeManager.currentCapacity;
        currentLevel = barnUpgradeManager.currentLevel;
        // TODO: load chicken list from save file
    }

    bool Upgrade()
    {
        int upgradeCost = barnUpgradeManager.CalculateUpgradeCost();

        if (!WalletManager.instance.CanAfford(upgradeCost, WalletManager.CurrencyType.COIN))
            return false;

        WalletManager.instance.DeductMoney(upgradeCost, WalletManager.CurrencyType.COIN);
        return barnUpgradeManager.UpgradeBarn();
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
        WalletManager.instance.AddMoney(soldChicken.marketValue, WalletManager.CurrencyType.COIN);
    }

    public void AddChicken(Chicken chicken)
    {
        chickenList.Add(chicken);
    }

    bool IsAtFullCapacity()
    {
        Assert.IsFalse(ChickenCount > currentCapacity);
        return ChickenCount == currentCapacity;
    }
}
