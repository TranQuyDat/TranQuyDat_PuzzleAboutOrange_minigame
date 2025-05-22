using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell 
{
    private Vector2 _pos;
    private int _x, _y;
    private bool _iswalkable;
    private CellItem _item;

    public Cell(Vector2 pos , int x , int y )
    {
        _pos = pos;
        _x = x;
        _y = y;
        _item = null;
        _iswalkable = true;
    }

    public void Clear()
    {
        _item =null;
        _iswalkable = true;
    }
    public void Attach(CellItem item)
    {
        _item = item;
        _iswalkable = false;
    }
    public Vector2 Pos => _pos;
    public int X => _x;
    public int Y => _y;
    public bool IsWalkable => _iswalkable;
    public CellItem Item => _item;
}
