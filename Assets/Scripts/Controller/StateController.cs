using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController 
{
    private IState _curState;
    public IState CurState =>_curState;
    public void ChangeState(IState state)
    {
        _curState?.Exit();
        _curState = state;
        _curState.Enter();
    }
}
