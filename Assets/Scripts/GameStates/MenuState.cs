using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState :GameState, IState
{
    private MenuUi _menuUi;

    public MenuState(UiManager uiManager)
    {
        _uiManager = uiManager;
    }
    public void Enter()
    {
        _menuUi = _uiManager.ShowUi<MenuUi>(GameStateType.MENU);
        _uiManager.Bg.sprite = _menuUi.SpriteBg;
        _menuUi.BtnPlay.onClick.AddListener(btnPlay);
        _menuUi.BtnQuit.onClick.AddListener(btnQuit);
    }

    public void Excute()
    {
    }

    public void Exit()
    {
        _menuUi.BtnPlay.onClick.RemoveAllListeners();
        _menuUi.BtnQuit.onClick.RemoveAllListeners();
        _uiManager.HideUi(GameStateType.MENU, _menuUi.gameObject);
    }

    private void btnPlay()
    {
        _uiManager._stateCtrl.ChangeState(new HowToPlayState(_uiManager));
    }

    private void btnQuit()
    {
        Application.Quit();
    }

}
