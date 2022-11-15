using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurState { get; private set; }

    public void Init(PlayerState state)
    {
        CurState = state;
        CurState.Enter();
    }

    public void ChangeState(PlayerState state, params Object[] args)
    {
        CurState.Exit();
        CurState = state;
        CurState.Enter(args);
    }
}
