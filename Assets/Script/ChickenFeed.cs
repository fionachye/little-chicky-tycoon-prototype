using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFeed  {

    public const int 
        GRADE_S_MARKET_VALUE = 50,
        GRADE_A_MARKET_VALUE = 40,
        GRADE_B_MARKET_VALUE = 30,
        GRADE_C_MARKET_VALUE = 20;

    char grade;
    int marketValue;

    public ChickenFeed(char grade)
    {
        switch(grade)
        {
            case 'a':
                marketValue = GRADE_A_MARKET_VALUE;
                break;
            case 'b':
                marketValue = GRADE_B_MARKET_VALUE;
                break;
            case 'c':
                marketValue = GRADE_C_MARKET_VALUE;
                break;
            case 's':
                marketValue = GRADE_S_MARKET_VALUE;
                break;
        }
    }
}
