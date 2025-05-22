using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUi : MonoBehaviour
{
    [SerializeField]private Button _btnPlay;
    [SerializeField]private Button _btnQuit;
    [SerializeField] private Sprite _spriteBg;

    public Button BtnPlay =>_btnPlay;
    public Button BtnQuit =>_btnQuit;
    public Sprite SpriteBg =>_spriteBg;
}
