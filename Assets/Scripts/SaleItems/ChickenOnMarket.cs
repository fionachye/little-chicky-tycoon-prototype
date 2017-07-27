using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used by market only
public class ChickenOnMarket {

    public Chickens grade;
    public int marketValue;

    public ChickenOnMarket(Chickens grade)
    {
        this.grade = grade;

        switch (this.grade)
        {
            case Chickens.Grade_A:
                marketValue = 200;
                break;
            case Chickens.Grade_B:
                marketValue = 400;
                break;
            case Chickens.Grade_C:
                marketValue = 600;
                break;
            case Chickens.Grade_S:
                marketValue = 800;
                break;
        }
    }
}
