using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class downPanelUI : MonoBehaviour
{

    [SerializeField] private ButtonDepending[] myButtons;

    private int localMoney;
    private int localPriceLevel;

    private gameplaySettingsSO gs;
    private myData defaultData;
    private uiManager _uiManager;
    public void Init(myData _data, uiManager _manager)
    {
        _uiManager = _manager;
        gs = dependencyManager.Instance._gameplaySettings;
        UpdateData(_data);
        dependencyManager.Instance._playerManager.OnLevelsChange += UpdateData;
        dependencyManager.Instance._playerManager.OnMoneyChange += UpdateMoney;

    }

    void UpdateMoney(int _money)
    {
        defaultData.money = _money;
        UpdateData(defaultData);
    }
    void UpdateData(myData _data)
    {
        localMoney = _data.money;

        if (_data.priceLevel >= gs.priceProgression.Length - 1)
            myButtons[0].myButton.gameObject.SetActive(false);
        else
            myButtons[0].curPrice = gs.priceProgression[_data.priceLevel+1].price;

        if (_data.speedLevel >= gs.speedProgression.Length - 1)
            myButtons[1].myButton.gameObject.SetActive(false);
        else
            myButtons[1].curPrice = gs.speedProgression[_data.speedLevel+1].price;
       
        if (_data.sawmillLevel >= gs.sawmillProgression.Length - 1)
            myButtons[2].myButton.gameObject.SetActive(false);
        else
            myButtons[2].curPrice = gs.sawmillProgression[_data.sawmillLevel+1].price;

        myButtons[0].levelText.text = "Level " + (_data.priceLevel + 1).ToString();
        myButtons[1].levelText.text = "Level " + (_data.speedLevel + 1).ToString();
        myButtons[2].levelText.text = "Level " + (_data.sawmillLevel + 1).ToString();


        for (int i = 0; i < myButtons.Length; i++)
        {
            myButtons[i].priceText.text = myButtons[i].curPrice.ToString() + "$";
            if (localMoney>=myButtons[i].curPrice)
            {
                myButtons[i].myButton.enabled = true;
                myButtons[i].myButton.GetComponent<Image>().color = Color.white;
            }
            else
            {
                myButtons[i].myButton.enabled = false;
                myButtons[i].myButton.GetComponent<Image>().color = Color.grey;
            }
        }

        defaultData = _data;
    }


    public void PressPriceButton()
    {
        if (defaultData.priceLevel<gs.priceProgression.Length-1)
        {
            int price = gs.priceProgression[defaultData.priceLevel + 1].price;
            if(localMoney>=price)
                _uiManager.IncreasePlayerLevel(gameplaySettingsSO.PlayerLevels.price, price);
        }
        else
        {
            myButtons[0].myButton.gameObject.SetActive(false);
        }
    }

    public void PressSpeedButton()
    {
        if (defaultData.speedLevel<gs.speedProgression.Length-1)
        {
            int price = gs.speedProgression[defaultData.speedLevel + 1].price;
            if(localMoney>=price)
                _uiManager.IncreasePlayerLevel(gameplaySettingsSO.PlayerLevels.speed, price);
        }
        else
        {
            myButtons[1].myButton.gameObject.SetActive(false);
        }
    }

    public void PressSawmillButton()
    {
        if (defaultData.sawmillLevel<gs.sawmillProgression.Length-1)
        {
            int price = gs.sawmillProgression[defaultData.sawmillLevel + 1].price;
            if(localMoney>=price)
                _uiManager.IncreasePlayerLevel(gameplaySettingsSO.PlayerLevels.sawmill, price);
        }
        else
        {
            myButtons[2].myButton.gameObject.SetActive(false);
        }
    }
    
}

[System.Serializable]
public class ButtonDepending
{
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI priceText;
    public Button myButton;
    [HideInInspector]public int curPrice;
}