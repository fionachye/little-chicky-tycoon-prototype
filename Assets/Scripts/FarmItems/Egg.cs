using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg {

    public Eggs grade { get; private set; }
    public int marketValue { get; private set; }
    public float hatchRemainingTime { get; private set; }
    public DateTime hatchStartTime { get; private set; }
    public bool isHatching { get; private set; }

    // 120 minutes hatching time
    const int HATCH_TIME = 120;

    public Egg Initialize(Eggs grade)
    {
        switch(grade)
        {
            case Eggs.Grade_S:
                marketValue = 20;
                break;
            case Eggs.Grade_A:
                marketValue = 15;
                break;
            case Eggs.Grade_B:
                marketValue = 10;
                break;
            case Eggs.Grade_C:
                marketValue = 5;
                break;
        }

        isHatching = false;
        hatchRemainingTime = HATCH_TIME;
        return this;
    }

    public void StartHatch()
    {
        hatchStartTime = DateTime.UtcNow;
        isHatching = true;
    }

    public void SetHatchRemainingTime(float time)
    {
        hatchRemainingTime = time;
    }
	
    
}
