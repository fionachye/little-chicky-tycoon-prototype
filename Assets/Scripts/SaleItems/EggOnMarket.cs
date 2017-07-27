using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggOnMarket {

    public Eggs grade;
    public int marketValue;

    public EggOnMarket(Eggs grade)
    {
        this.grade = grade;
        switch (this.grade)
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
    }
}
