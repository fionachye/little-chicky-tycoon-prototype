using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedOnMarket {

    public Feeds grade;
    public int marketValue;

    public FeedOnMarket(Feeds grade)
    {
        this.grade = grade;

        switch (grade)
        {
            case Feeds.Grade_A:
                marketValue = 40;
                break;
            case Feeds.Grade_B:
                marketValue = 30;
                break;
            case Feeds.Grade_C:
                marketValue = 20;
                break;
            case Feeds.Grade_S:
                marketValue = 50;
                break;
        }
    }
	
}
