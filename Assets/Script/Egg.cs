using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg {

    public Chicken.Grade grade { get; private set; }
    public int marketValue { get; private set; }
    public float hatchRemainingTime { get; private set; }
    public DateTime hatchStartTime { get; private set; }
    public bool isHatching { get; private set; }

    // 120 minutes hatching time
    const int HATCH_TIME = 120;

    public void Initialize(Chicken.Grade grade)
    {
        switch(grade)
        {
            case Chicken.Grade.S:
                marketValue = 20;
                break;
            case Chicken.Grade.A:
                marketValue = 15;
                break;
            case Chicken.Grade.B:
                marketValue = 10;
                break;
            case Chicken.Grade.C:
                marketValue = 5;
                break;
        }

        isHatching = false;
        hatchRemainingTime = HATCH_TIME;
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
