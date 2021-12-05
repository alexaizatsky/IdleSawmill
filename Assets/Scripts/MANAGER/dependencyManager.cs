using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class dependencyManager : MonoBehaviour
{
    public static dependencyManager Instance;

    public playerManager _playerManager;
    public levelManager _levelManager;
    public uiManager _uiManager;
    public gameplaySettingsSO _gameplaySettings;

    public UnityEvent StartIntitialization;
    private void Awake()
    {
        if (Instance != null )
        {
            Destroy( gameObject );
            return;
        }
        Instance = this;
        StartIntitialization.Invoke();
    }
}
