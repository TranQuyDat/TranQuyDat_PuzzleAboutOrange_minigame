using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEditor.Timeline.TimelinePlaybackControls;
public enum Direction { Up, Down, Left, Right }
public class PuzzleGame :MonoBehaviour
{
    private bool _isWin;
    private bool _isLose;
    private Board _board;
    private GameObject _boardObj;
    private Dictionary<string, GameObject> _uis;
    private List<Orange> _oranges;
    private Vector2 swipeStart, swipeEnd;
    private Transform _content;
    private float _time = 45.1f;
    public bool IsWin => _isWin;
    public bool IsLose => _isLose;
    public float TimeOut => _time;

    private bool _isStart;
    private void Awake()
    {
        _oranges = new List<Orange>();
        _isStart= false;
    }
    private void Update()
    {
        if (!_isStart) return;
        if(_time > 0 && !_isWin)
        {
            _time -= Time.deltaTime;
            
            if(_time <= 0 ) _isLose = true;
        }
        HandleSwipeInput();
    }

    public void Init(LevelData levelData , Dictionary<string ,GameObject> uis ,Transform content)
    {
        _isStart = false;
        _uis = uis;
        _isWin = false;
        _isLose = false;
        _content = content;
        _time = 45.1f;
        //create board
        _boardObj = PoolObject.GetPool("" + PrefabName.BOARD, uis["" + PrefabName.BOARD]);
        _boardObj.transform.position = Vector3.zero;
        _boardObj.transform.SetParent(_content);
        _boardObj.SetActive(true);
        SpriteRenderer sr = _boardObj.GetComponent<SpriteRenderer>();

        float ppu = sr.sprite.pixelsPerUnit;
        Vector4 b = sr.sprite.border / ppu;

        Vector2 safeSize = (Vector2)sr.bounds.size - new Vector2(b.x + b.z, b.y + b.w);

        _board = new Board(levelData.BoardWitdh , levelData.BoardHeight);
        _board.Init(_boardObj.transform.position, safeSize);
        InitLevel(levelData);
    }    
    private void InitLevel(LevelData lvData)
    {
       
        createOrange(PrefabName.ORANGE_TOP_LEFT);
        createOrange(PrefabName.ORANGE_TOP_RIGHT);
        createOrange(PrefabName.ORANGE_BOTTOM_LEFT);
        createOrange(PrefabName.ORANGE_BOTTOM_RIGHT);
        
        foreach (Vector2Int v in lvData.BlocksXy)
        {
            GameObject obj = PoolObject.GetPool("" + PrefabName.BLOCK, _uis["" + PrefabName.BLOCK]);
            obj.transform.SetParent(_content);
            obj.SetActive(true);
            Block block = obj.GetComponent<Block>();
            if(block == null) block = obj.AddComponent<Block>();
            block.Attach(_board.Cells[v.x, v.y]);
        }

        StartShuffleOrange(lvData);
    }


    public void HandleSwipeInput()
    {
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Began) swipeStart = t.position;
        else if (t.phase == TouchPhase.Ended)
        {
            swipeEnd = t.position;
            Vector2 delta = swipeEnd - swipeStart;
            if (delta.magnitude < 50) return;

            Direction dir = Mathf.Abs(delta.x) > Mathf.Abs(delta.y)
                ? (delta.x > 0 ? Direction.Right : Direction.Left)
                : (delta.y > 0 ? Direction.Up : Direction.Down);

            MoveOrangesOneStep(dir);
        }
    }

    public void MoveOrangesOneStep(Direction dir)
    {
        _oranges.Sort((a, b) => dir switch
        {
            Direction.Left => a.Cell.X.CompareTo(b.Cell.X),      // x nhỏ đi trước
            Direction.Right => b.Cell.X.CompareTo(a.Cell.X),      // x lớn đi trước
            Direction.Up => b.Cell.Y.CompareTo(a.Cell.Y),      // y lớn đi trước
            Direction.Down => a.Cell.Y.CompareTo(b.Cell.Y),      // y nhỏ đi trước
            _ => 0
        });

        foreach (var o in _oranges)
        {
            int x = o.Cell.X;
            int y = o.Cell.Y;

            switch (dir)
            {
                case Direction.Left: x -= 1; break;
                case Direction.Right: x += 1; break;
                case Direction.Up: y += 1; break;
                case Direction.Down: y -= 1; break;
            }

            if (x < 0 || y < 0 || x >= _board.Width || y >= _board.Height)
                continue;

            var next = _board.Cells[x, y];
            if (next.IsWalkable && next.Item == null)
            {
                o.Swap(next);
            }
        }

        CheckWin();
    }

    private void createOrange(PrefabName name)
    {
        GameObject obj = PoolObject.GetPool("" + name, _uis["" + name]);
        obj.transform.SetParent(_content);
        obj.SetActive(true);

        Orange oran = obj.GetComponent<Orange>();
        if(oran == null) oran = obj.AddComponent<Orange>();

        if(name == PrefabName.ORANGE_TOP_RIGHT)
        {
            oran.Attach(_board.Cells[3, 1]);
            oran.Type = OrangeType.TOP_RIGHT;
        }
        else if (name == PrefabName.ORANGE_TOP_LEFT)
        {
            oran.Attach(_board.Cells[2, 1]);
            oran.Type= OrangeType.TOP_LEFT;
        }
        else if(name == PrefabName.ORANGE_BOTTOM_RIGHT)
        {
            oran.Attach(_board.Cells[3, 0]);
            oran.Type = OrangeType.BOTTOM_RIGHT;
        }
        else
        {
            oran.Attach(_board.Cells[2, 0]);
            oran.Type = OrangeType.BOTTOM_LEFT;
        }

        _oranges.Add(oran);
    }
    public void StartShuffleOrange(LevelData lvData)
    {
        StartCoroutine(ShuffleOrangeStepByStep(lvData, 3, 0.3f));
    }

    IEnumerator ShuffleOrangeStepByStep(LevelData lvData, int steps, float delay)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < steps; i++)
        {
            foreach (var orange in _oranges)
            {
                Cell next = GetRandomNearbyFreeCell(orange.Cell, lvData);
                if (next != null) orange.Swap(next);
            }
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForEndOfFrame();
        _isStart = true;
    }

    Cell GetRandomNearbyFreeCell(Cell cur, LevelData lv)
    {
        List<Cell> list = new();
        int x = cur.X, y = cur.Y;
        Vector2Int[] dirs = { new(-1, 0), new(1, 0), new(0, 1), new(0, -1) };

        foreach (var d in dirs)
        {
            int nx = x + d.x, ny = y + d.y;
            if (nx >= 0 && ny >= 0 && nx < lv.BoardWitdh && ny < lv.BoardHeight)
            {
                var cell = _board.Cells[nx, ny];
                if (cell.IsWalkable && cell.Item == null) list.Add(cell);
            }
        }
        return list.Count == 0 ? null : list[Random.Range(0, list.Count)];
    }
    public void CheckWin()
    {
        Orange tl = _oranges.Find(o => o.Type == OrangeType.TOP_LEFT);
        if (tl == null) return;
        int x = tl.Cell.X, y = tl.Cell.Y;

        bool win = _oranges.Exists(o => o.Type == OrangeType.TOP_RIGHT && o.Cell.X == x + 1 && o.Cell.Y == y) &&
                   _oranges.Exists(o => o.Type == OrangeType.BOTTOM_LEFT && o.Cell.X == x && o.Cell.Y == y - 1) &&
                   _oranges.Exists(o => o.Type == OrangeType.BOTTOM_RIGHT && o.Cell.X == x + 1 && o.Cell.Y == y - 1);

        if (win)
        {
            _isWin = true;
            Debug.Log("You win!");
        }
    }

    public void Clear()
    {
        foreach(Transform t in _content)
        {
            if (t.GetComponent<Block>() !=null)
            {
                PoolObject.DeActiveObj(t.gameObject, "" + PrefabName.BLOCK);
                continue;
            }
            else if (t.TryGetComponent<Orange>(out var orange))
            {
                string prefabName = orange.GetPrefabName();
                PoolObject.DeActiveObj(t.gameObject, "" + prefabName);
                continue;
            }
            else
            {
                PoolObject.DeActiveObj(t.gameObject, "" + PrefabName.BOARD);
            }
        }

        _oranges.Clear();
    }


    bool isGizmos = false;
    private void OnDrawGizmos()
    {
        if (!isGizmos) return;
        if (_board == null || _board.Cells == null) return;
        Gizmos.color = Color.yellow;
        foreach (Cell c in _board.Cells)
        {
            Gizmos.DrawCube(c.Pos, Vector3.one);
        }
    }
}
