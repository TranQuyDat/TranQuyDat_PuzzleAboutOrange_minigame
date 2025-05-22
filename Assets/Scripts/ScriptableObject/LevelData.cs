using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    [SerializeField] private int _levelid;
    [SerializeField] private Vector2Int[] _blocks_Xy;
    [SerializeField] private int _board_Witdh;
    [SerializeField] private int _board_Height;
    [SerializeField] private Vector2Int _orangeTopRight_Xy;
    [SerializeField] private Vector2Int _orangeTopLeft_Xy;
    [SerializeField] private Vector2Int _orangeBottomRight_Xy;
    [SerializeField] private Vector2Int _orangeBottomLeft_Xy;

    public int Levelid => _levelid;
    public Vector2Int[] BlocksXy => _blocks_Xy;
    public int BoardWitdh => _board_Witdh;
    public int BoardHeight => _board_Height;

    public Vector2Int OrangeTopRightXY => _orangeTopRight_Xy;
    public Vector2Int OrangeTopLeftXY => _orangeTopLeft_Xy;
    public Vector2Int OrangeBottomRightXY => _orangeBottomRight_Xy;
    public Vector2Int OrangeBottomLeftXY => _orangeBottomLeft_Xy;
}
