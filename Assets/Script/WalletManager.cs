using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletManager : MonoBehaviour {

    public static WalletManager instance;
    public static int gemCount { get; private set; }
    public static int coinCount { get; private set; }

    public enum CurrencyType
    {
        COIN,
        GEM
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int amount, CurrencyType type)
    {
        switch(type)
        {
            case CurrencyType.COIN:
                coinCount += amount;
                break;
            case CurrencyType.GEM:
                gemCount += amount;
                break;
        }
    }

    public void DeductMoney(int amount, CurrencyType type)
    {
        switch (type)
        {
            case CurrencyType.COIN:
                coinCount -= amount;
                break;
            case CurrencyType.GEM:
                gemCount -= amount;
                break;
        }
    }

    public bool CanAfford(int amount, CurrencyType type)
    {
        if (type == CurrencyType.COIN)
        {
            return coinCount >= amount;
        }

        if (type == CurrencyType.GEM)
        {
            return gemCount >= amount;
        }
        
        return false;
    }

    
}
