using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameplaySettings", menuName = "ScriptableObjects/gameplaySettings", order = 1)]
public class gameplaySettingsSO : ScriptableObject
{
    public float lumberjackMoveSpeed;
    public float lumberjackChopSpeed;
    public int woodPrice;
    [Header("---------- PREFABS -----------")]
    public GameObject levelPrefab;
    public GameObject sawmillPrefab;
    public GameObject lumberjackPrefab;
    public GameObject treePrefab;

    [Header("-------------- PROGRESS --------------")]
    public ProgressionElement[] priceProgression;
    public ProgressionElement[] speedProgression;
    public ProgressionElement[] sawmillProgression;
    
    public enum PlayerLevels
    {
        price,
        speed,
        sawmill,
    }
}

[System.Serializable]
public class ProgressionElement
{
    public float multiplier;
    public int price;
}