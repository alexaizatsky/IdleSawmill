using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    [SerializeField] private Transform visualTransform;
    private lumberjack myLumberjack;
    private forest myForest;
    [HideInInspector]public bool isBusy;
    [HideInInspector]public bool lastTree;
    
    public void Init(forest _forest)
    {
        myForest = _forest;
    }
    public void StartCuttingMe(lumberjack _lumberjack)
    {
        myLumberjack = _lumberjack;
        myLumberjack.OnCuttingTreeUpdate += CuttingUpdate;
    }
    public void CuttingUpdate(float _progress)
    {
        if (_progress<1)
        {
            visualTransform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), Vector3.zero, _progress);
        }
        else
        {
            DestroyMe();
        }
    }

    void DestroyMe()
    {
        myLumberjack.OnCuttingTreeUpdate -= CuttingUpdate;
        if(!lastTree)
            myForest.DestroyTreeFromList(this);
        Destroy(this.gameObject);
    }
}
