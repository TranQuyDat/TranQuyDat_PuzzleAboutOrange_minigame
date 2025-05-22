using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PuzzleGame _puzzle;
    public LevelManager _levelManager;
    public UiManager _uiManager;
    [SerializeField]private GameData _gameData;
    [SerializeField] private Transform _gameContent;

    private void OnValidate()
    {
        if(_uiManager!=null && _levelManager!=null) return;
        _uiManager = this.GetComponent<UiManager>();
        if(_uiManager == null) _uiManager = this.AddComponent<UiManager>();
        _levelManager = this.GetComponent<LevelManager>();
        if(_levelManager == null) _levelManager = this.AddComponent<LevelManager>();
    }
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        var state = _uiManager.CurStateType;

        if (state == GameStateType.SELECT_LEVEL &&
            !_gameData._levelUnlockeds.SequenceEqual(_levelManager.LevelUnLocked))
            _gameData._levelUnlockeds = _levelManager.LevelUnLocked.ToList();

        if (state == GameStateType.MENU && _puzzle) { _puzzle.Clear(); Destroy(_puzzle); return; }
        if (state != GameStateType.GAMEPLAY) return;

        if (!_puzzle) GameStart();
        if (_gameData._isRestart) { GameRestart(); _gameData._isRestart = false; }

        if (_puzzle.IsWin) GameWin();
        else if (_puzzle.IsLose) GameOver();

        _gameData._timeOut = _puzzle.TimeOut;
    }


    private void GameStart()
    {
        _puzzle = this.AddComponent<PuzzleGame>();
        _puzzle.Init(_levelManager.CurLevelData, _uiManager.Uis ,_gameContent);
    }
    private void GameRestart()
    {
        _puzzle.Clear();
        _puzzle.Init(_levelManager.CurLevelData, _uiManager.Uis, _gameContent);
    }
    private void GameOver()
    {
        _gameData._curCountStar = 3;
        _uiManager._stateCtrl.ChangeState(new GameOverState(_uiManager));
        _puzzle.Clear();
        Destroy(_puzzle);
    }
    private void GameWin()
    {
        _gameData._curCountStar = calCoutStar(_gameData._timeOut);
        _uiManager._stateCtrl.ChangeState(new GameWinState(_uiManager));
        _levelManager.UpdateLevelUnlocked(_gameData._curLevelId,calCoutStar(_gameData._timeOut));
        _puzzle.Clear();
        Destroy(_puzzle);
    }

    public void OnSave(string key , string value)
    {
        PlayerPrefs.SetString(key, value);
    }
        
    private int calCoutStar(float time)
    {
        if (time >= 30f) return 3;
        if (time >= 15f) return 2;
        if (time > 0f) return 1;
        return 0;
    }
   
}
