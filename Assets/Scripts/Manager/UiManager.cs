using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public enum GameStateType
{
    MENU,HOW_TO_PLAY,SELECT_LEVEL, GAMEPLAY,GAMEOVER,GAMEWIN
}
public enum PrefabName
{
    START_WIN,STAR_LOSE,BLOCK,LEVEL,BOARD,
    ORANGE_TOP_RIGHT,ORANGE_TOP_LEFT,
    ORANGE_BOTTOM_RIGHT,ORANGE_BOTTOM_LEFT,
}
public class UiManager : MonoBehaviour
{
    [SerializeField] private Image _bg;
    [SerializeField] private Transform _CanvasUi;
    [SerializeField] private GameData _gameData;
    [SerializeField] public UnityEvent<int> OnChangeLevel;
    GameStateType _curStateType;
    public StateController _stateCtrl { get; private set; }
    public GameData GameData=> _gameData;
    public Image Bg => _bg;
    public GameStateType CurStateType => _curStateType;
    private Dictionary<string, GameObject> _uis;

    public Dictionary<string ,GameObject> Uis => _uis;
    private void Awake()
    {
        _uis = new Dictionary<string, GameObject>();
        //GAME STATE PREFAB
        _uis["" + GameStateType.MENU] = Resources.Load<GameObject>(Contants.PATH_PREFAB_MENU);
        _uis["" + GameStateType.HOW_TO_PLAY] = Resources.Load<GameObject>(Contants.PATH_PREFAB_HOWTOPLAY);
        _uis["" + GameStateType.SELECT_LEVEL] = Resources.Load<GameObject>(Contants.PATH_PREFAB_SELECTLEVEL);
        _uis["" + GameStateType.GAMEPLAY] = Resources.Load<GameObject>(Contants.PATH_PREFAB_GAMEPLAY);
        _uis["" + GameStateType.GAMEWIN] = Resources.Load<GameObject>(Contants.PATH_PREFAB_GAMEWIN);
        _uis["" + GameStateType.GAMEOVER] = Resources.Load<GameObject>(Contants.PATH_PREFAB_GAMELOSE);

        //OTHER PREFAB
        _uis["" + PrefabName.LEVEL] = Resources.Load<GameObject>(Contants.PATH_PREFAB_LEVEL);
        _uis["" + PrefabName.BLOCK] = Resources.Load<GameObject>(Contants.PATH_PREFAB_BLOCK);
        _uis["" + PrefabName.BOARD] = Resources.Load<GameObject>(Contants.PATH_PREFAB_BOARD);
        _uis["" + PrefabName.STAR_LOSE] = Resources.Load<GameObject>(Contants.PATH_PREFAB_STAR_LOSE);
        _uis["" + PrefabName.START_WIN] = Resources.Load<GameObject>(Contants.PATH_PREFAB_STAR_WIN);
        _uis["" + PrefabName.ORANGE_TOP_RIGHT] = Resources.Load<GameObject>(Contants.PATH_PREFAB_ORANGE_TOP_RIGHT);
        _uis["" + PrefabName.ORANGE_TOP_LEFT] = Resources.Load<GameObject>(Contants.PATH_PREFAB_ORANGE_TOP_LEFT);
        _uis["" + PrefabName.ORANGE_BOTTOM_RIGHT] = Resources.Load<GameObject>(Contants.PATH_PREFAB_ORANGE_BOTTOM_RIGHT);
        _uis["" + PrefabName.ORANGE_BOTTOM_LEFT] = Resources.Load<GameObject>(Contants.PATH_PREFAB_ORANGE_BOTTOM_LEFT);

        _stateCtrl = new StateController();
    }
    void Start()
    {
        _stateCtrl.ChangeState(new MenuState(this));
    }

    private void Update()
    {
        _stateCtrl.CurState.Excute();
    }

    public T ShowUi<T>(GameStateType stateType)
    {
        GameObject obj = PoolObject.GetPool(stateType.ToString(), _uis["" + stateType]);
        obj.SetActive(true);
        obj.transform.SetParent(_CanvasUi,false);
        _curStateType = stateType;
        T t = obj.GetComponent<T>();
        return t;
    }
    public void HideUi(GameStateType stateType , GameObject obj)
    {
        obj.SetActive(false);
        PoolObject.DeActiveObj(obj, stateType.ToString());
    }
}
