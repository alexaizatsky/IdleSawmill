using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelPrefab : MonoBehaviour
{
    public Transform[] sawmillsPoints;
    public forest myForest;

    public void StartInit()
    {
        myForest.StartInit();
    }
}
