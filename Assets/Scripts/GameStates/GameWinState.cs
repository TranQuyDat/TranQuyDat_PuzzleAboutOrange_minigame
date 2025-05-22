using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinState : GameState,IState
{
    private GameWinUi _gameWinUi;

    public GameWinState(UiManager uiManager)
    {
        _uiManager = uiManager;
    }

    public void Enter()
    {
        _gameWinUi = _uiManager.ShowUi<GameWinUi>(GameStateType.GAMEWIN);
        _uiManager.Bg.sprite = _gameWinUi.SpriteBg;
        _gameWinUi.BtnHome.onClick.AddListener(btnHome);
        _gameWinUi.BtnRestart.onClick.AddListener(btnReStart);
        _gameWinUi.BtnNext.onClick.AddListener(btnNext);
        
        // add starwin follow score

        for (int i = 0; i < _uiManager.GameData._curCountStar; i++) 
        {
            GameObject obj = PoolObject.GetPool("" + PrefabName.START_WIN, _uiManager.Uis["" + PrefabName.START_WIN]);
            obj.SetActive(true);
            _gameWinUi.AddStarToContent(obj);
        }
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
        _gameWinUi.BtnHome.onClick.RemoveAllListeners();
        _gameWinUi.BtnRestart.onClick.RemoveAllListeners();
        _gameWinUi.BtnNext.onClick.RemoveAllListeners();
        clearAllContens();
        _uiManager.HideUi(GameStateType.GAMEWIN, _gameWinUi.gameObject);
    }

    private void clearAllContens()
    {
        foreach (Transform t in _gameWinUi.StarContent)
        {
            PoolObject.DeActiveObj(t.gameObject, "" + PrefabName.START_WIN);
        }
    }
}
