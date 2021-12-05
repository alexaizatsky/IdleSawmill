using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MoneyValueChange(int _value);
public delegate void LevelsValueChange(myData _newData);

public class playerManager : MonoBehaviour
{
    private int money;
    private int priceLevel;
    private int speedLevel;
    private int sawmillLevel;

    private myDataLoading myLoader;

    public event MoneyValueChange OnMoneyChange;
    public event LevelsValueChange OnLevelsChange;
    public void StartInit(myData _startData, myDataLoading _loader)
    {
        money = _startData.money;
        priceLevel = _startData.priceLevel;
        speedLevel = _startData.speedLevel;
        sawmillLevel = _startData.sawmillLevel;
        myLoader = _loader;
        dependencyManager.Instance._uiManager.OnIncPlayerLevel += IncreasePlayerLevel;
    }

    public void IncreaseMoney(int _amount)
    {
        money += _amount;
        if (OnMoneyChange != null)
            OnMoneyChange(money);
        SaveData();
    }

    void ReduceMoney(int _amount)
    {
        money -= _amount;
        if (OnMoneyChange != null)
            OnMoneyChange(money);
        SaveData();
    }
    public void IncreasePlayerLevel(gameplaySettingsSO.PlayerLevels _type, int _moneyReduce)
    {
        switch (_type)
        {    
            case gameplaySettingsSO.PlayerLevels.price:
                priceLevel++;
                break;
            case gameplaySettingsSO.PlayerLevels.speed:
                speedLevel++;
                break;
            case gameplaySettingsSO.PlayerLevels.sawmill:
                sawmillLevel++;
                break;
        }
        ReduceMoney(_moneyReduce);
        LevelsChanged();
        SaveData();
    }

    void LevelsChanged()
    {
        myData newData = new myData();
        newData.money = money;
        newData.priceLevel = priceLevel;
        newData.speedLevel = speedLevel;
        newData.sawmillLevel = sawmillLevel;
        if (OnLevelsChange != null)
            OnLevelsChange(newData);
    }
    void SaveData()
    {
        myData saveData = new myData();
        saveData.money = money;
        saveData.priceLevel = priceLevel;
        saveData.speedLevel = speedLevel;
        saveData.sawmillLevel = sawmillLevel;
        myLoader.WriteToJson(saveData);
    }
    
}
