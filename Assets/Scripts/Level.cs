using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject _icon_lock;
    [SerializeField] private Button _btnClick;
    [SerializeField] private TextMeshProUGUI _nameLevel;
    [SerializeField] private Transform _starContent;
    private UiManager _uiManager;
    private int _levelId;

    public void Init(UiManager uiManager , int levelId)
    {
        _uiManager = uiManager;
        _levelId = levelId;
        var lvUnlock = _uiManager.GameData._levelUnlockeds.FirstOrDefault(lvUnlock => lvUnlock._levelId == levelId);
        if (lvUnlock == null)
        {
            _icon_lock.SetActive(true);
            _btnClick.interactable = false;
            _nameLevel.gameObject.SetActive(false);
            return;
        }
        _icon_lock.SetActive(false);
        _btnClick.interactable = true;
        _starContent.gameObject.SetActive(true);
        _nameLevel.text = "Level "+levelId;
        _nameLevel.gameObject.SetActive(true);
        if (lvUnlock._countStar <=0) return;
        addStarToContent(lvUnlock._countStar);
    }

    private void addStarToContent(int starCount)
    {
        for(int i = 0; i < starCount; i++)
        {
            GameObject obj = PoolObject.GetPool("" + PrefabName.START_WIN, _uiManager.Uis["" + PrefabName.START_WIN]);
            obj.transform.SetParent(_starContent, false);
            obj.SetActive(true);
        }
    }
    public void Clear()
    {
        foreach(Transform t in _starContent)
        {
            PoolObject.DeActiveObj(t.gameObject, "" + PrefabName.START_WIN);
        }
    }

    public void Onclick()
    {
        _uiManager.OnChangeLevel.Invoke(_levelId);
        _uiManager._stateCtrl.ChangeState(new GamePlayState(_uiManager));
    }
}
