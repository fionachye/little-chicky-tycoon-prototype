using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillUpgradeManager : MonoBehaviour {

    public int currentCapacity { get; private set; }
    public int currentLevel { get; private set; }

    const int INITIAL_CAPACITY = 10;
    const int INITIAL_LEVEL = 1;
    const int UPGRADE_COST = 1200;
    const int MAX_LEVEL = 3;

    public void SetupWindmill()
    {
        // TODO: load from xml if loadingFromFile is true
        currentCapacity = INITIAL_CAPACITY;
        currentLevel = INITIAL_LEVEL;
    }

    public bool UpgradeWindmill()
    {
        int levelUpgrade = currentLevel + 1;

        if (levelUpgrade > MAX_LEVEL)
        {
            // fail to upgrade
            return false;
        }

        currentLevel++;
        currentCapacity = currentLevel * 10;
        WindmillController.instance.CurrentCapacity = currentCapacity;
        WindmillController.instance.CurrentLevel = currentLevel;
        return true;
    }

    public int CalculateUpgradeCost()
    {
        int upgradeCost = UPGRADE_COST * currentLevel;
        return upgradeCost;
    }
}
