using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketUpgradeManager : MonoBehaviour {

    public int currentLevel { get; private set; }

    const int INITIAL_LEVEL = 1;
    const int UPGRADE_COST = 1800;
    const int MAX_LEVEL = 4;

    public void SetupMarket()
    {
        currentLevel = INITIAL_LEVEL;

        // set initial probability
    }

    public bool UpgradeMarket()
    {
        int levelUpgrade = currentLevel + 1;

        if (levelUpgrade > MAX_LEVEL)
        {
            // fail to upgrade
            return false;
        }

        currentLevel++;
        MarketController.instance.CurrentLevel = currentLevel;
        return true;
    }

    public int CalculateUpgradeCost()
    {
        int upgradeCost = UPGRADE_COST * currentLevel;
        return upgradeCost;
    }
}
