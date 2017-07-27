using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatcheryUpgradeManager : MonoBehaviour {

    public int incubatorCount { get; private set; }
    public int currentLevel { get; private set; }

    public const int INITIAL_INCUBATOR_COUNT = 1;
    public const int INITIAL_LEVEL = 1;
    public const int UPGRADE_COST = 800;
    public const int BUILD_COST = 800;
    public const int MAX_LEVEL = 3;

    public void SetupHatchery()
    {
        // todo: load from xml if loadingFromFile = true
        incubatorCount = INITIAL_INCUBATOR_COUNT;
        currentLevel = INITIAL_LEVEL;
    }

    public bool UpgradeHatchery()
    {
        int levelUpgrade = currentLevel + 1;

        if (levelUpgrade > MAX_LEVEL)
        {
            // fail to upgrade
            return false;
        }

        currentLevel++;
        incubatorCount++;
        HatcheryController.instance.CurrentLevel = currentLevel;
        HatcheryController.instance.IncubatorCount = incubatorCount;
        return true;
    }

    public int CalculateUpgradeCost()
    {
        int upgradeLevel = currentLevel + 1;
        int upgradeCost = UPGRADE_COST * upgradeLevel;
        return upgradeCost;
    }
}
