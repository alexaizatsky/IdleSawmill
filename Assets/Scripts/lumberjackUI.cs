using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lumberjackUI : MonoBehaviour
{
    [SerializeField] private GameObject canvasObj;
    [SerializeField] private Image fillImage;

    public void Init()
    {
        canvasObj.SetActive(false);
    }

    public void ActivateProgressUI()
    {
        fillImage.fillAmount = 0;
        canvasObj.SetActive(true);
    }

    public void ProgressUIUpdate(float _progress)
    {
        fillImage.fillAmount = _progress;
    }

    public void DeactivateProgressUI()
    {
        canvasObj.SetActive(false);
    }
}
