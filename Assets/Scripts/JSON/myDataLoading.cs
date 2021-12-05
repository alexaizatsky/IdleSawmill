using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class myDataLoading : MonoBehaviour
{
   
    private string dataToJson;
    private string dataFromJson;
    [SerializeField] private string defaultData;
    private myData readMyData, writeMyData;
    [SerializeField] private string fileName = "myData.json";
    private string filePath;

    public void StartInit()
    {
        if (Application.isEditor)
            filePath = Path.Combine(Application.dataPath, fileName);
        else
            filePath = Path.Combine(Application.persistentDataPath + "/", fileName);
   
        if (File.Exists(filePath))
        {
            ReadFromJson();
        }
        else
        {
            File.WriteAllText(filePath, defaultData);
            ReadFromJson();
        }
        writeMyData = new myData();
        dependencyManager.Instance._playerManager.StartInit(readMyData, this);
        dependencyManager.Instance._levelManager.StartInit(readMyData);
        dependencyManager.Instance._uiManager.StartInit(readMyData);
    }


    void ReadFromJson()
    {
        dataFromJson = File.ReadAllText(filePath);
        readMyData = JsonUtility.FromJson<myData>(dataFromJson);
    }
    
    public void WriteToJson(myData _data)
    {
        writeMyData = _data;
        dataToJson = JsonUtility.ToJson(writeMyData);
        File.WriteAllText(filePath, dataToJson);
    }
}
