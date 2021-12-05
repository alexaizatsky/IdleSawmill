using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public delegate void CuttingTreeUpdate(float _progress);
[RequireComponent(typeof(lumberjackUI))]
public class lumberjack : MonoBehaviour
{
   public enum State
   {
      wait,
      moveToForest,
      chopTree,
      moveToSaw,
   }

   public State myState;
   public event CuttingTreeUpdate OnCuttingTreeUpdate;
   private forest myForest;
   private tree nextTree;

   private float moveSpeed;
   private float chopSpeed;
   private int woodPrice;

   private lumberjackUI _lumberjackUi;
   private sawmill mySawmill;
   private gameplaySettingsSO gs;
   public void Init(sawmill _sawmill, forest _forest, myData _data)
   {
      gs = dependencyManager.Instance._gameplaySettings;
      myForest = _forest;
      mySawmill = _sawmill;
      ChangeData(_data);
      _lumberjackUi = GetComponent<lumberjackUI>();
      SetState(State.moveToForest);
      
   }

   public void ChangeData(myData _data)
   {
      if (_data.speedLevel < gs.speedProgression.Length)
      {
         moveSpeed = gs.lumberjackMoveSpeed*gs.speedProgression[_data.speedLevel].multiplier;
         chopSpeed = gs.lumberjackChopSpeed*gs.speedProgression[_data.speedLevel].multiplier;
      }
      else
      {
         Debug.LogWarning("INCORRECT SAVE DATA");
         moveSpeed = gs.lumberjackMoveSpeed;
         chopSpeed = gs.lumberjackChopSpeed;
      }

      if (_data.priceLevel<gs.priceProgression.Length)
      {
         woodPrice = Mathf.RoundToInt( gs.woodPrice*gs.priceProgression[_data.priceLevel].multiplier);
      }
      else
      {
         Debug.LogWarning("INCORRECT SAVE DATA");
         woodPrice = gs.woodPrice;
      }
   }

   void SetState(State s)
   {
      myState = s;
      if (s == State.moveToForest)
      {
         nextTree = myForest.GetClosetTree(this.transform.position);
         Vector3 dir = (this.transform.position - nextTree.transform.position).normalized;
         Vector3 nextPos = nextTree.transform.position +dir*0.8f;
         nextPos.y = 0;
         float dist = (nextTree.transform.position - this.transform.position).magnitude;
         float t = dist / moveSpeed;
         this.transform.DOMove(nextPos, t).OnComplete(FinTreeMove);
      }
      else if(s == State.chopTree)
      {
         nextTree.StartCuttingMe(this);
         StartCoroutine(ChopTree(10/chopSpeed));

      }
      else if (s == State.moveToSaw)
      {
         Vector3 newPos = mySawmill.transform.position + new Vector3(0, 0, 1);
         float dist = (newPos- this.transform.position).magnitude;
         float t = dist / moveSpeed;
         this.transform.DOMove(newPos, t).OnComplete(FinSawMove);
      }
   }

   void FinTreeMove()
   {
      SetState(State.chopTree);
   }

   void FinChopTree()
   {
      SetState(State.moveToSaw);
   }

   void FinSawMove()
   {
      mySawmill.GetIncome(woodPrice);
      SetState(State.moveToForest);
   }
   IEnumerator ChopTree(float _time)
   {
      float timer = 0;
      _lumberjackUi.ActivateProgressUI();
      while (timer<=_time)
      {
         timer += Time.deltaTime;
         float prog = Mathf.InverseLerp(0, _time, timer);
         _lumberjackUi.ProgressUIUpdate(prog);
         if (OnCuttingTreeUpdate != null)
            OnCuttingTreeUpdate(prog);
         yield return null;
      }
      if (OnCuttingTreeUpdate != null)
         OnCuttingTreeUpdate(1);
      _lumberjackUi.DeactivateProgressUI();
      FinChopTree();
   }
   
}
