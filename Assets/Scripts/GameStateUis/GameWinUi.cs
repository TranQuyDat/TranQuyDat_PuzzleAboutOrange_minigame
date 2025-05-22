using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinUi : MonoBehaviour
{
    [SerializeField] private Button _btnHome;
    [SerializeField] private Button _btnNext;
    [SerializeField] private Button _btnRestart;
    [SerializeField] private Transform _starContent;
    [SerializeField] private Sprite _spriteBg;
    public Button BtnHome => _btnHome;
    public Button BtnNext => _btnNext;
    public Button BtnRestart => _btnRestart;
    public Transform StarContent => _starContent;
    public Sprite SpriteBg => _spriteBg;


    public void AddStarToContent(GameObject star)
    {
        star.transform.SetParent(_starContent, false);
    }
}
