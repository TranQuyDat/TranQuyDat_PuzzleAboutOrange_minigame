using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState 
{
    protected UiManager _uiManager;
    protected void btnHome()
    {
        _uiManager._stateCtrl.ChangeState(new MenuState(_uiManager));
    }
    protected virtual void btnReStart()
    {
        _uiManager._stateCtrl.ChangeState(new GamePlayState(_uiManager));
    }
    protected void btnNext()
    {
        int nextLevelId = _uiManager.GameData._curLevelId + 1;
        if (nextLevelId > _uiManager.GameData._levelDatas.Length)
        {
            _uiManager._stateCtrl.ChangeState(new SelectLevelState(_uiManager));
            return;
        }
        _uiManager.GameData.OnchangeLevel.Invoke(nextLevelId);
        _uiManager._stateCtrl.ChangeState(new GamePlayState(_uiManager));
    }
}
