using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrainOnMarket {

    public Grains grade;
    public int marketValue;

    public GrainOnMarket(Grains grade)
    {
        this.grade = grade;

        switch (grade)
        {
            case Grains.Grade_A:
                marketValue = 15;
                break;
            case Grains.Grade_B:
                marketValue = 10;
                break;
            case Grains.Grade_C:
                marketValue = 5;
                break;
            case Grains.Grade_S:
                marketValue = 20;
                break;
        }
    }
}
