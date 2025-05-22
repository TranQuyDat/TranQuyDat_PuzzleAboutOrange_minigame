using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayUi : MonoBehaviour
{
    [SerializeField] private Button _btnBack;
    [SerializeField] private TextMeshProUGUI _txtHTP;
    [SerializeField] private Sprite _spriteBg;

    public Sprite SpriteBg => _spriteBg;
    public Button BtnBack => _btnBack;

    public void SetTxtHowToPlay(string txt)
    {
        _txtHTP.text = txt; 
    }
}
