using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class SelectLevelState : GameState, IState
{
    private SelectLevelUi _selectLevelUi;
    public SelectLevelState(UiManager uiManager)
    {
        _uiManager = uiManager;

    }
    public void Enter()
    {
        _selectLevelUi = _uiManager.ShowUi<SelectLevelUi>(GameStateType.SELECT_LEVEL);
        _uiManager.Bg.sprite = _selectLevelUi.SpriteBg;
        _selectLevelUi.BtnBack.onClick.AddListener(btnBack);

        foreach (LevelData lvdata in _uiManager.GameData._levelDatas) 
        {
            GameObject obj = PoolObject.GetPool("" + PrefabName.LEVEL, _uiManager.Uis["" + PrefabName.LEVEL]);
            obj.SetActive(true);
            Level lv = obj.GetComponent<Level>();
            lv.Init(_uiManager, lvdata.Levelid);
            _selectLevelUi.AddLevelToContent(obj);
        }
    }

    public void Excute()
    {
        
    }

    public void Exit()
    {
        _selectLevelUi.BtnBack.onClick.RemoveAllListeners();
        clearObjLevels();
        _uiManager.HideUi(GameStateType.SELECT_LEVEL,_selectLevelUi.gameObject);
    }

    private void clearObjLevels()
    {
        foreach (Transform t in _selectLevelUi.LevelsContent)
        {
            t.GetComponent<Level>().Clear();
            PoolObject.DeActiveObj(t.gameObject, ""+PrefabName.LEVEL);
        }
    }

    private void btnBack()
    {
        _uiManager._stateCtrl.ChangeState(new MenuState(_uiManager));
    }
}
