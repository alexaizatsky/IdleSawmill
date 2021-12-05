using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class upPanelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private int localMoney;
    private Coroutine smoothCor;
    public void Init( int _money)
    {
        localMoney = _money;
        moneyText.text = _money.ToString() + "$";
        dependencyManager.Instance._playerManager.OnMoneyChange += MoneyUpdate;
    }

    private void OnDisable()
    {
        dependencyManager.Instance._playerManager.OnMoneyChange -= MoneyUpdate;
    }

    void MoneyUpdate(int _money)
    {
        if (localMoney!=_money)
        {
            if(smoothCor!=null)
                StopCoroutine(smoothCor);
            smoothCor = StartCoroutine(SmoothChangeMoney(_money, 0.3f));
        }
    }

    IEnumerator SmoothChangeMoney(int _amount, float _time)
    {
        float timer = 0;
        int startMoney = localMoney;
        while (timer<_time)
        {
            timer += Time.deltaTime;
            float prog = Mathf.InverseLerp(0, _time, timer);
            localMoney = Mathf.RoundToInt(Mathf.Lerp(startMoney, _amount, prog));
            moneyText.text = localMoney.ToString() + "$";
            yield return null;
        }
        localMoney = _amount;
        moneyText.text = localMoney.ToString() + "$";
    }
}
