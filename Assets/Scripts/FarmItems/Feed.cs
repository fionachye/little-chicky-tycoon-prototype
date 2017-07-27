using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feed {

    public Feeds grade { get; private set; }
    public int marketValue { get; private set; }

    public void Initialize(Feeds grade)
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
