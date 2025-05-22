using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelUi : MonoBehaviour
{
    [SerializeField] private Transform _levelsContent;
    [SerializeField] private Sprite _spriteBg;
    [SerializeField] private Button _btnBack;
    public Sprite SpriteBg => _spriteBg;
    public Button BtnBack => _btnBack;
    public Transform LevelsContent => _levelsContent;
    public void AddLevelToContent(GameObject level)
    {
        level.transform.SetParent(_levelsContent, false);
    }
}
