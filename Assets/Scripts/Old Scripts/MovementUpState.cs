using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class MovementUpState : State
{
    float duration = 1f;

    private float deltaTime = 0.0f;
    private GameObject m_gameObject;

    public MovementUpState(FSM fsm, GameObject gameObject) : base(fsm)
    {
        m_gameObject = gameObject;
    }

    public override void OnEnter()
    {
        deltaTime = Time.deltaTime;
        base.OnEnter();
    }

    public override void OnUpdate()
    {            
        deltaTime += Time.deltaTime;
        if (deltaTime > duration)
        {
            Directions nextid = Directions.NORTH;
            State nextState = m_fsm.GetState(nextid);
            m_fsm.SetCurrentState(nextState);
        }

        if (m_gameObject != null)
        {
            m_gameObject.transform.position += Vector3.up * Time.deltaTime;
        }
    }
}
