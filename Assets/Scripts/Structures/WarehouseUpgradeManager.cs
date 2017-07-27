using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseUpgradeManager : MonoBehaviour {

    public int currentCapacity { get; private set; }
    public int currentLevel { get; private set; }

    const int INITIAL_CAPACITY = 100;
    const int INITIAL_LEVEL = 1;
    const int UPGRADE_COST = 800;
    const int MAX_LEVEL = 3;

    public void SetupWarehouse()
    {
        // todo: load from xml if loadingFromFile = true
        currentCapacity = INITIAL_CAPACITY;
        currentLevel = INITIAL_LEVEL;
    }
    
    public bool UpgradeWarehouse()
    {
        int levelUpgrade = currentLevel + 1;

        if (levelUpgrade > MAX_LEVEL)
        {
            // fail to upgrade
            return false;
        }

        currentLevel++;
        currentCapacity += INITIAL_CAPACITY;
        WarehouseController.instance.CurrentLevel = currentLevel;
        WarehouseController.instance.CurrentStorageCapacity = currentCapacity;
        return true;
    }

    public int CalculateUpgradeCost()
    {
        int upgradeLevel = currentLevel + 1;
        int upgradeCost = UPGRADE_COST * upgradeLevel;
        return upgradeCost;
    }
}
