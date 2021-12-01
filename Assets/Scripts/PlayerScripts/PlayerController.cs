using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerPatterns;

public class PlayerController : MonoBehaviour
{
    FSM m_fsm = new FSM();

    private void Start()
    {
        m_fsm.AddState(PlayerState.WALKING, new Walking(m_fsm));
        m_fsm.AddState(PlayerState.RUNNING, new Running(m_fsm));
    }
}

public class Walking : State
{
    public Walking(FSM fsm) : base(fsm)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        //walking code
    }

    public override void OnExit()
    {

    }
}

public class Running : State
{
    public Running(FSM fsm) : base(fsm)
    {

    }
}
