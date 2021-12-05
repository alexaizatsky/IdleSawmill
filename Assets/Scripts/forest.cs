using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class forest : MonoBehaviour
{
    public List<tree> myTrees = new List<tree>();
    private GameObject treePrefab;
    
    
    public void StartInit()
    {
       treePrefab = dependencyManager.Instance._gameplaySettings.treePrefab;
       GenerateNewTrees();
    }



    void GenerateNewTrees()
    {
        for (int h = 0; h < myTrees.Count; h++)
        {
            if (!myTrees[h].isBusy)
                Destroy(myTrees[h].gameObject);
            else
                myTrees[h].lastTree = true;
        }
       myTrees.Clear();
       for (int i = 0; i < 5; i++)
       {
           for (int j = 0; j < 5; j++)
           {
               GameObject c = Instantiate(treePrefab);
               c.transform.SetParent(this.transform);
               c.transform.localPosition = new Vector3( j*1.5f-3, 0,i);
               tree _tree = c.GetComponent<tree>();
               myTrees.Add(_tree);
               _tree.Init(this);
               
           }
       }
    }

    public void DestroyTreeFromList(tree _tree)
    {
        if (myTrees.Contains(_tree))
        {
            for (int i = 0; i < myTrees.Count; i++)
            {
                if (myTrees[i] == _tree)
                {
                    myTrees.RemoveAt(i);
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("NU SUCH TREE IN LIST");
        }
    }
    public tree GetClosetTree(Vector3 _pos)
    {
        if(myTrees.Count<3)
            GenerateNewTrees();
        float dist = 1000;
        int arrNumb = 0;
        for (int i = 0; i < myTrees.Count; i++)
        {
            if ((myTrees[i].transform.position - _pos).magnitude < dist && !myTrees[i].isBusy)
            {
                dist = (myTrees[i].transform.position - _pos).magnitude;
                arrNumb = i;
            }
        }

        myTrees[arrNumb].isBusy = true;
        return myTrees[arrNumb];

    }
}
