using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    private levelPrefab _levelPrefab;
    private forest myForest;
    private gameplaySettingsSO gs;
    private List<sawmill> mySawmills = new List<sawmill>();
    public void StartInit(myData _startData)
    {
        gs = dependencyManager.Instance._gameplaySettings;
        _levelPrefab = GenerateLevel();
        myForest = _levelPrefab.myForest;
        myForest.StartInit();
        GenerateStartSawmills(_startData);
        dependencyManager.Instance._playerManager.OnLevelsChange += PlayerLevelsChange;
    }

    levelPrefab GenerateLevel()
    {
        GameObject c = Instantiate(gs.levelPrefab);
        c.transform.position = Vector3.zero;
        c.transform.eulerAngles = Vector3.zero;
        c.transform.localScale = new Vector3(1,1,1);
        return c.GetComponent<levelPrefab>();

    }

    void GenerateStartSawmills(myData _data)
    {
        int sawCount = _data.sawmillLevel + 1;
        sawCount = Mathf.Clamp(sawCount,0, _levelPrefab.sawmillsPoints.Length);

        for (int i = 0; i < sawCount; i++)
        {
            GameObject c = Instantiate(gs.sawmillPrefab);
            c.transform.position = _levelPrefab.sawmillsPoints[i].position;
            c.transform.eulerAngles = _levelPrefab.sawmillsPoints[i].eulerAngles;
            sawmill _sawmill = c.GetComponent<sawmill>();
            _sawmill.Init(myForest, _data);
            mySawmills.Add(c.GetComponent<sawmill>());
        }
    }

    void PlayerLevelsChange(myData _data)
    {
        if (_data.sawmillLevel+1>mySawmills.Count)
        {
           
                GameObject c = Instantiate(gs.sawmillPrefab);
                c.transform.position = _levelPrefab.sawmillsPoints[_data.sawmillLevel].position;
                c.transform.eulerAngles = _levelPrefab.sawmillsPoints[_data.sawmillLevel].eulerAngles;
                sawmill _sawmill = c.GetComponent<sawmill>();
                _sawmill.Init(myForest, _data);
                mySawmills.Add(c.GetComponent<sawmill>());
            
        }

        foreach (var saw in mySawmills)
        {
            saw.ChangeData(_data);
        }
    }
}
