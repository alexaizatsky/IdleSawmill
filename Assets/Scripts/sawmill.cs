using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sawmill : MonoBehaviour
{
    private lumberjack myLumberjack;
    public void Init(forest _forest, myData _data)
    {
        gameplaySettingsSO gs = dependencyManager.Instance._gameplaySettings;
        if (myLumberjack == null)
        {
            GameObject c = Instantiate(gs.lumberjackPrefab);
            c.transform.position = this.transform.position + new Vector3(0, 0, 1);
            c.transform.eulerAngles = Vector3.zero;
            c.transform.localScale = new Vector3(1,1,1);
            myLumberjack = c.GetComponent<lumberjack>();
            myLumberjack.Init(this, _forest, _data);
        }
    }


    public void ChangeData(myData _data)
    {
        myLumberjack.ChangeData(_data);
    }
    public void GetIncome(int _money)
    {
        dependencyManager.Instance._playerManager.IncreaseMoney(_money);
        StartCoroutine(ScalePush(0.3f, 1.2f));
    }


    IEnumerator ScalePush(float _time, float mult)
    {
        float timer = 0;
        Vector3 starScale = this.transform.localScale;
        while (timer<_time)
        {
            timer += Time.deltaTime;
            if (timer<_time/2)
            {
                float prog = Mathf.InverseLerp(0, _time / 2, timer);
                this.transform.localScale = Vector3.Lerp(starScale, starScale * mult, prog);
            }
            else
            {
                float prog = Mathf.InverseLerp( _time / 2, _time, timer);
                this.transform.localScale = Vector3.Lerp(starScale*mult, starScale, prog);
            }
            
            yield return null;
        }

        this.transform.localScale = starScale;
    }
}
