using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState :GameState, IState
{
    private GamePlayUi _gamePlayUi;
    public GamePlayState(UiManager uiManager)
    {
        _uiManager = uiManager;
    }
    public void Enter()
    {
        _gamePlayUi = _uiManager.ShowUi<GamePlayUi>(GameStateType.GAMEPLAY);
        _uiManager.Bg.sprite = _gamePlayUi.SpriteBg;
        _gamePlayUi.BtnHome.onClick.AddListener(btnHome);
        _gamePlayUi.BtnRestart.onClick.AddListener(btnReStart);
    }

    public void Excute()
    {
        if (_uiManager.GameData._timeOut <= 0)
        {
            return;
        }
        string timeTxt = timeFloatToTimeTxt(_uiManager.GameData._timeOut);
        _gamePlayUi.setTxtTime(timeTxt);
    }

    public void Exit()
    {
        _gamePlayUi.BtnHome.onClick.RemoveAllListeners();
        _gamePlayUi.BtnRestart.onClick.RemoveAllListeners();
        _uiManager.HideUi(GameStateType.GAMEPLAY,_gamePlayUi.gameObject);
    }

    protected override void btnReStart()
    {
        _uiManager.GameData._isRestart = true;
    }

    private string timeFloatToTimeTxt(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
