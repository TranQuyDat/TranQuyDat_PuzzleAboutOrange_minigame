using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData[] _levelDatas;
    private List<LevelUnlock> _levelUnlocked;
    LevelData _curLevelData;
    public GameData _gameData;
    public LevelData CurLevelData => _curLevelData;
    public LevelUnlock[] LevelUnLocked => _levelUnlocked.ToArray();
    private void Awake()
    {
        if (!PlayerPrefs.HasKey(Contants.KEY_SAVE_UNLOCK_LEVEL))
        {
            LevelUnlock lvUnlock = new LevelUnlock(1, 0);
            _levelUnlocked.Clear();
            _levelUnlocked.Add(lvUnlock);

            GameSaveData saveData = new GameSaveData();
            saveData._levelUnlockeds = _levelUnlocked;

            string json = JsonUtility.ToJson(saveData, true);
            _gameData.OnSave?.Invoke(Contants.KEY_SAVE_UNLOCK_LEVEL, json);
        }
        else
        {
            LoadGame();
        }

        _levelDatas = new LevelData[3]
        {
           Resources.Load<LevelData>(Contants.PATH_DATA_LEVEL1),
           Resources.Load<LevelData>(Contants.PATH_DATA_LEVEL2),
           Resources.Load<LevelData>(Contants.PATH_DATA_LEVEL3),
        };
        _gameData._levelDatas = _levelDatas;
        _gameData.OnchangeLevel = ChangeLevel;
    }
    public void ChangeLevel(int levelId)
    {
        if (levelId <= 0) return;
        _curLevelData = _levelDatas[levelId - 1];
        _gameData._curLevelId = levelId;
        UpdateLevelUnlocked(levelId, 0);
    }

    public void UpdateLevelUnlocked(int levelid , int countStart)
    {
        if (countStart > 3) return;

        GameSaveData saveData = new GameSaveData();

        LevelUnlock lvUnlock =_levelUnlocked.FirstOrDefault(lv =>lv._levelId == levelid);
        if(lvUnlock!=null && lvUnlock._countStar < countStart)
        {
            lvUnlock._countStar = countStart;
        }
        else if(lvUnlock == null)
        {
            lvUnlock = new LevelUnlock(levelid, countStart);
            _levelUnlocked.Add(lvUnlock);
        }
        else return;
        saveData._levelUnlockeds = _levelUnlocked;
        string json = JsonUtility.ToJson(saveData, true);
        _gameData.OnSave.Invoke(Contants.KEY_SAVE_UNLOCK_LEVEL, json);
    }

    private void LoadGame()
    {
        string json = PlayerPrefs.GetString(Contants.KEY_SAVE_UNLOCK_LEVEL);
        GameSaveData loadData = JsonUtility.FromJson<GameSaveData>(json);
        _levelUnlocked = loadData._levelUnlockeds;
    }

}
