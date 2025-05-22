using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayState :GameState, IState
{
    private HowToPlayUi _howtoplayUi;

    public HowToPlayState(UiManager uiManager)
    {
        _uiManager = uiManager;
    }
    public void Enter()
    {
        _howtoplayUi = _uiManager.ShowUi<HowToPlayUi>(GameStateType.HOW_TO_PLAY);
        _uiManager.Bg.sprite = _howtoplayUi.SpriteBg;
        _howtoplayUi.BtnBack.onClick.AddListener(btnBack);
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
        _howtoplayUi.BtnBack.onClick.RemoveAllListeners();
        _uiManager.HideUi(GameStateType.HOW_TO_PLAY, _howtoplayUi.gameObject);
    }

    private void btnBack()
    {
        _uiManager._stateCtrl.ChangeState(new SelectLevelState(_uiManager));
    }
}
