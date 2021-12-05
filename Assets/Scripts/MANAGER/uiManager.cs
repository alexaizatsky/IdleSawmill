using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public delegate void IncreasePlayerLevel(gameplaySettingsSO.PlayerLevels _type, int _reduceMoney);

public class uiManager : MonoBehaviour
{
    [SerializeField] private upPanelUI _upPanelUi;
    [SerializeField] private downPanelUI _downPanelUi;
    public event IncreasePlayerLevel OnIncPlayerLevel;
    public void StartInit(myData _startData)
    {
        _upPanelUi.gameObject.SetActive(true);
        _upPanelUi.Init(_startData.money);
        _downPanelUi.gameObject.SetActive(true);
        _downPanelUi.Init(_startData, this);
    }

    public void IncreasePlayerLevel(gameplaySettingsSO.PlayerLevels _type, int _reduceMoney)
    {
        if (OnIncPlayerLevel != null)
            OnIncPlayerLevel(_type, _reduceMoney);
    }
}
