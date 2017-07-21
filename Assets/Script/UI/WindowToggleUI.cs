using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowToggleUI : MonoBehaviour {

    Vector2 barnWindowClosePosition,
        barnWindowOpenPosition,
        warehouseWindowClosePosition,
        warehouseWindowOpenPosition,
        hatcheryWindowClosePosition,
        hatcheryWindowOpenPosition,
        windmillWindowClosePosition,
        windmillWindowOpenPosition,
        marketWindowClosePosition,
        marketWindowOpenPosition,
        upgradeClosePosition,
        upgradeOpenPosition,
        exitClosePosition,
        exitOpenPosition;
    RectTransform barnWindow,
        warehouseWindow,
        hatcheryWindow,
        windmillWindow,
        marketWindow,
        upgradeBtn,
        exitBtn;
    bool barnWindowOpen = false,
        warehouseWindowOpen = false,
        hatcheryWindowOpen = false,
        windmillWindowOpen = false,
        marketWindowOpen = false,
        windowToggle = false;
    float windowToggleSpeed = 0f;


	void Start ()
    {
        barnWindow = GameObject.Find("BarnWindow").GetComponent<RectTransform>();
        barnWindowClosePosition = new Vector2(barnWindow.anchoredPosition.x, barnWindow.anchoredPosition.y);
        barnWindowOpenPosition = new Vector2(barnWindow.anchoredPosition.x, 0f);
        warehouseWindow = GameObject.Find("WarehouseWindow").GetComponent<RectTransform>();
        warehouseWindowClosePosition = new Vector2(warehouseWindow.anchoredPosition.x, warehouseWindow.anchoredPosition.y);
        warehouseWindowOpenPosition = new Vector2(warehouseWindow.anchoredPosition.x, 0f);
        hatcheryWindow = GameObject.Find("HatcheryWindow").GetComponent<RectTransform>();
        hatcheryWindowClosePosition = new Vector2(hatcheryWindow.anchoredPosition.x, hatcheryWindow.anchoredPosition.y);
        hatcheryWindowOpenPosition = new Vector2(hatcheryWindow.anchoredPosition.x, 0f);
        windmillWindow = GameObject.Find("WindmillWindow").GetComponent<RectTransform>();
        windmillWindowClosePosition = new Vector2(windmillWindow.anchoredPosition.x, windmillWindow.anchoredPosition.y);
        windmillWindowOpenPosition = new Vector2(windmillWindow.anchoredPosition.x, 0f);
        marketWindow = GameObject.Find("MarketWindow").GetComponent<RectTransform>();
        marketWindowClosePosition = new Vector2(marketWindow.anchoredPosition.x, marketWindow.anchoredPosition.y);
        marketWindowOpenPosition = new Vector2(marketWindow.anchoredPosition.x, 0f);

        upgradeBtn = GameObject.Find("UpgradeBtn").GetComponent<RectTransform>();
        upgradeClosePosition = new Vector2(upgradeBtn.anchoredPosition.x, upgradeBtn.anchoredPosition.y);
        upgradeOpenPosition = new Vector2(0f, upgradeBtn.anchoredPosition.y);
        exitBtn = GameObject.Find("CloseBtn").GetComponent<RectTransform>();
        exitClosePosition = new Vector2(exitBtn.anchoredPosition.x, exitBtn.anchoredPosition.y);
        exitOpenPosition = new Vector2(0f, exitBtn.anchoredPosition.y);
    }
	
	void Update ()
    {
		if (windowToggle)
        {
            if (windowToggleSpeed < 1f)
            {
                windowToggleSpeed += 0.2f;
                
                if (!barnWindowOpen && !warehouseWindowOpen && !hatcheryWindowOpen && !windmillWindowOpen && !marketWindowOpen)
                {
                    barnWindow.anchoredPosition = Vector2.Lerp(barnWindow.anchoredPosition, barnWindowClosePosition, windowToggleSpeed);
                    warehouseWindow.anchoredPosition = Vector2.Lerp(warehouseWindow.anchoredPosition, warehouseWindowClosePosition, windowToggleSpeed);
                    hatcheryWindow.anchoredPosition = Vector2.Lerp(hatcheryWindow.anchoredPosition, hatcheryWindowClosePosition, windowToggleSpeed);
                    windmillWindow.anchoredPosition = Vector2.Lerp(windmillWindow.anchoredPosition, windmillWindowClosePosition, windowToggleSpeed);
                    marketWindow.anchoredPosition = Vector2.Lerp(marketWindow.anchoredPosition, marketWindowClosePosition, windowToggleSpeed);
                    exitBtn.anchoredPosition = Vector2.Lerp(exitBtn.anchoredPosition, exitClosePosition, windowToggleSpeed);
                    upgradeBtn.anchoredPosition = Vector2.Lerp(upgradeBtn.anchoredPosition, upgradeClosePosition, windowToggleSpeed);
                }
                else
                {
                    exitBtn.anchoredPosition = Vector2.Lerp(exitBtn.anchoredPosition, exitOpenPosition, windowToggleSpeed);
                    upgradeBtn.anchoredPosition = Vector2.Lerp(upgradeBtn.anchoredPosition, upgradeOpenPosition, windowToggleSpeed);
                    if (barnWindowOpen)
                        barnWindow.anchoredPosition = Vector2.Lerp(barnWindow.anchoredPosition, barnWindowOpenPosition, windowToggleSpeed);
                    else if (warehouseWindowOpen)
                        warehouseWindow.anchoredPosition = Vector2.Lerp(warehouseWindow.anchoredPosition, warehouseWindowOpenPosition, windowToggleSpeed);
                    else if (hatcheryWindowOpen)
                        hatcheryWindow.anchoredPosition = Vector2.Lerp(hatcheryWindow.anchoredPosition, hatcheryWindowOpenPosition, windowToggleSpeed);
                    else if (windmillWindowOpen)
                        windmillWindow.anchoredPosition = Vector2.Lerp(windmillWindow.anchoredPosition, windmillWindowOpenPosition, windowToggleSpeed);
                    else if (marketWindowOpen)
                        marketWindow.anchoredPosition = Vector2.Lerp(marketWindow.anchoredPosition, windmillWindowOpenPosition, windowToggleSpeed);
                }
            }
            else
            {
                windowToggleSpeed = 0f;
                windowToggle = false;
            }
        }
	}

    public void BarnWindowToggle()
    {
        if (warehouseWindowOpen || hatcheryWindowOpen || windmillWindowOpen || marketWindowOpen)
        {
            //do nothing
        }
        else if (barnWindowOpen)
        {
            barnWindowOpen = false;
            windowToggle = true;
        }
        else
        {
            barnWindowOpen = true;
            windowToggle = true;
        }
    }

    public void WarehouseWindowToggle()
    {
        if (barnWindowOpen || hatcheryWindowOpen || windmillWindowOpen || marketWindowOpen)
        {
            //do nothing
        }
        else if (warehouseWindowOpen)
        {
            warehouseWindowOpen = false;
            windowToggle = true;
        }
        else
        {
            warehouseWindowOpen = true;
            windowToggle = true;
        }
    }

    public void HatcheryWindowToggle()
    {
        if (warehouseWindowOpen || barnWindowOpen || windmillWindowOpen || marketWindowOpen)
        {
            //do nothing
        }
        else if (hatcheryWindowOpen)
        {
            hatcheryWindowOpen = false;
            windowToggle = true;
        }
        else
        {
            hatcheryWindowOpen = true;
            windowToggle = true;
        }
    }

    public void WindmillWindowToggle()
    {
        if (warehouseWindowOpen || hatcheryWindowOpen || barnWindowOpen || marketWindowOpen)
        {
            //do nothing
        }
        else if (windmillWindowOpen)
        {
            windmillWindowOpen = false;
            windowToggle = true;
        }
        else
        {
            windmillWindowOpen = true;
            windowToggle = true;
        }
    }

    public void MarketWindowToggle()
    {
        if (warehouseWindowOpen || hatcheryWindowOpen || windmillWindowOpen || barnWindowOpen)
        {
            //do nothing
        }
        else if (marketWindowOpen)
        {
            marketWindowOpen = false;
            windowToggle = true;
        }
        else
        {
            marketWindowOpen = true;
            windowToggle = true;
        }
    }

    public void CloseWindow()
    {
        barnWindowOpen = false;
        warehouseWindowOpen = false;
        hatcheryWindowOpen = false;
        windmillWindowOpen = false;
        marketWindowOpen = false;
        windowToggle = true;
    }
}
