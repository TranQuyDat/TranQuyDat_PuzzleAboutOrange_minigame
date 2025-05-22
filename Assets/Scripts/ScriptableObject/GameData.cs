using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameData : ScriptableObject
{
    public LevelData[] _levelDatas;
    public List<LevelUnlock> _levelUnlockeds; // can save
    public int _curLevelId;
    public int _curCountStar;
    public float _timeOut;//  thoi gian level
    public bool _isRestart = false;

    [SerializeField] private UnityStringStringEvent _onSave;
    public Action<int> OnchangeLevel;
    public UnityStringStringEvent OnSave => _onSave;
}
[Serializable]
public class LevelUnlock
{
    public int _levelId;
    public int _countStar = -1;

    public LevelUnlock(int LevelId, int CountStar)
    {
        _levelId = LevelId;
        _countStar = CountStar;
    }
}
[Serializable]
public class  GameSaveData
{
    public List<LevelUnlock> _levelUnlockeds ;
}
[System.Serializable]
public class UnityStringStringEvent : UnityEvent<string, string> { }