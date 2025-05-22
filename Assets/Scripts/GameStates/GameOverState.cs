using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : GameState,IState
{
    private GameOverUi _gameOverUi;

    public GameOverState(UiManager uiManager)
    {
        _uiManager = uiManager;
    }
    public void Enter()
    {
        _gameOverUi = _uiManager.ShowUi<GameOverUi>(GameStateType.GAMEOVER);
        _uiManager.Bg.sprite = _gameOverUi.SpriteBg;
        _gameOverUi.BtnHome.onClick.AddListener(btnHome);
        _gameOverUi.BtnRestart.onClick.AddListener(btnReStart);
        _gameOverUi.BtnNext.onClick.AddListener(btnNext);
        // add 3 starlost
        for (int i = 0; i < _uiManager.GameData._curCountStar; i++)
        {
            GameObject obj = PoolObject.GetPool("" + PrefabName.STAR_LOSE, _uiManager.Uis["" + PrefabName.STAR_LOSE]);
            obj.SetActive(true);
            _gameOverUi.AddStarToContent(obj);
        }
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
        _gameOverUi.BtnHome.onClick.RemoveAllListeners();
        _gameOverUi.BtnRestart.onClick.RemoveAllListeners();
        _gameOverUi.BtnNext.onClick.RemoveAllListeners();
        clearAllContens();
        _uiManager.HideUi(GameStateType.GAMEOVER,_gameOverUi.gameObject);
    }
    private void clearAllContens()
    {
        foreach (Transform t in _gameOverUi.StarContent)
        {
            PoolObject.DeActiveObj(t.gameObject, "" + PrefabName.STAR_LOSE);
        }
    }
}
