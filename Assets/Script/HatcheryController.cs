using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatcheryController : MonoBehaviour {

    public static HatcheryController instance;
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
    // usable only if barn is not currently full
    bool isUsable;

    int incubatorCount, currentLevel;
    float probabilityRareEgg;
    int currentHatchingCount;
    List<Egg> eggs;
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
        HatchEggs();    
    }

    void SetupHatchery()
    {

    }

    void Upgrade()
    {

    }

    void HatchEggs()
    {
        // check back every minute
        if (DateTime.UtcNow.Minute < lastSimulationTime.Minute)
            return;

        foreach(Egg e in eggs)
        {
            if (e.isHatching)
            {
                float remainingTime = e.hatchRemainingTime;
                if (remainingTime <= 0)
                {
                    OnEggHatched(e);
                }
                else
                {
                    // update hatch remaining time
                    e.SetHatchRemainingTime(remainingTime-1);
                }
            }
        }
    }

    void HatchEggsOffline()
    {

    }

    void OnEggHatched(Egg egg)
    {
        ChickenManager.instance.InitializeChicken(egg.grade);
        eggs.Remove(egg);
    }

    int GetHatchingEggCOunt()
    {
        int count = 0;
        foreach(Egg e in eggs)
        {
            if (e.isHatching)
                count++;
        }

        return count;
    }

    int GetTotalEggCount()
    {
        return eggs.Count;
    }




}
