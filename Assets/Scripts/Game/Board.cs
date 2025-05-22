using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board 
{
    private int _width, _height;
    private Cell[,] _cells;
    public int Width => _width;
    public int Height => _height;
    public Cell[,] Cells =>_cells;
    public Board(int width, int height) 
    {
        _width = width;
        _height = height;
    }

    public void Init(Vector2 center,Vector2 sizeObj)
    {
        float cellSizeX = sizeObj.x/_width;
        float cellSizeY = sizeObj.y/_height;
        _cells = new Cell[_width, _height];
        Vector2 start = center - new Vector2( (_width - 1) * cellSizeX / 2f,(_height - 1) * cellSizeY / 2f);
        for (int i = 0; i < _width; i++)
        {
            for(int j =0; j< _height; j++)
            {
                Vector2 pos = start + new Vector2(i*cellSizeX, j*cellSizeY);
                _cells[i, j] = new Cell(pos,i,j);
            }
        }
    }
}
