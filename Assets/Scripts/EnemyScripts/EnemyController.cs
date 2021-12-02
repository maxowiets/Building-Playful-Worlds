using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyPatterns;

public class EnemyController : MonoBehaviour
{
    FSM m_fsm = new FSM();

    private void Start()
    {
        m_fsm.AddState(typeof(Idle), new Idle(m_fsm));
        m_fsm.AddState(typeof(Patrol), new Patrol(m_fsm));
        m_fsm.AddState(typeof(Chase), new Chase(m_fsm));
        m_fsm.AddState(typeof(Kill), new Kill(m_fsm));

        m_fsm.SetCurrentState(m_fsm.GetState(typeof(Idle)));
    }
}

public class Idle : State
{
    float idleTimer;
    float idleTime;
    public Idle(FSM fsm) : base(fsm)
    {

    }

    public override void OnEnter()
    {
        idleTime = Time.time;
    }

    public override void OnUpdate()
    {
        idleTime += Time.deltaTime;
        if (idleTime >= Time.time + idleTimer)
        {
            m_fsm.SetCurrentState(m_fsm.GetState(typeof(Patrol)));
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {

    }
}

public class Patrol : State
{
    public Patrol(FSM fsm) : base(fsm)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        //walking code
    }

    public override void OnFixedUpdate()
    {
        //walking code
    }

    public override void OnExit()
    {

    }
}

public class Chase : State
{
    public Chase(FSM fsm) : base(fsm)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        //walking code
    }

    public override void OnFixedUpdate()
    {
        //walking code
    }

    public override void OnExit()
    {

    }
}

public class Kill : State
{
    public Kill(FSM fsm) : base(fsm)
    {

    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        //walking code
    }

    public override void OnFixedUpdate()
    {
        //walking code
    }

    public override void OnExit()
    {

    }
}