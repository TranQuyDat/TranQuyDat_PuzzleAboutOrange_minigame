using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUi : MonoBehaviour
{
    [SerializeField] private Button _btnHome;
    [SerializeField] private Button _btnRestart;
    [SerializeField] private TextMeshProUGUI _txtTime;
    [SerializeField] private Sprite _spriteBg;

    public Button BtnHome => _btnHome;
    public Button BtnRestart => _btnRestart;

    public Sprite SpriteBg => _spriteBg;



    public void setTxtTime(string txt)
    {
        _txtTime.text = txt;
    }
}
