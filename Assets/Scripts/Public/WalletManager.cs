using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : MonoBehaviour {

    public static WalletManager instance;
    public static int gemCount { get; private set; }
    public static int coinCount { get; private set; }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int amount, Currency type)
    {
        switch(type)
        {
            case Currency.Coin:
                coinCount += amount;
                break;
            case Currency.Gem:
                gemCount += amount;
                break;
        }
    }

    public void DeductMoney(int amount, Currency type)
    {
        switch (type)
        {
            case Currency.Coin:
                coinCount -= amount;
                break;
            case Currency.Gem:
                gemCount -= amount;
                break;
        }
    }

    public bool CanAfford(int amount, Currency type)
    {
        if (type == Currency.Coin)
        {
            return coinCount >= amount;
        }

        if (type == Currency.Gem)
        {
            return gemCount >= amount;
        }
        
        return false;
    }
}