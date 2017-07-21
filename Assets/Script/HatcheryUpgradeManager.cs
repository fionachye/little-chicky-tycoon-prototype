using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatcheryUpgradeManager : MonoBehaviour {

    public int incubatorCount { get; private set; }
    public int currentLevel { get; private set; }

    const int INITIAL_INCUBATOR_COUNT = 1;
    const int INITIAL_LEVEL = 1;
    const int UPGRADE_COST = 800;
    const int BUILD_COST = 800;
    const int MAX_LEVEL = 3;

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
}
