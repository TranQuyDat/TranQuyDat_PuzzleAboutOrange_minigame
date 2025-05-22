using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class CellItem : MonoBehaviour
{
    private Cell _cell;
    public Cell Cell => _cell;
    public void Attach(Cell cell)
    {
        _cell = cell;
        _cell.Attach(this);
        this.transform.position = cell.Pos;
    }
    public void Remove(Cell cell)
    {
        _cell.Clear();
        _cell = null;
    }

    public void Swap(Cell cell)
    {
        _cell.Clear();
        _cell = cell;
        _cell.Attach(this);
        this.transform.position = cell.Pos;
    }
}
