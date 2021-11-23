using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class MovementStates : State
{
    float duration = 1f;

    private Directions m_Directions;
    private float deltaTime = 0.0f;
    private GameObject m_gameObject;

    public MovementStates(FSM fsm, GameObject gameObject, Directions directions = Directions.NORTH) : base(fsm)
    {
        m_gameObject = gameObject;
        m_Directions = directions;
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
            Directions nextid;
            State nextState;
            switch (m_Directions)
            {
                case Directions.NORTH:
                    nextid = Directions.EAST;
                    nextState = m_fsm.GetState(nextid);
                    m_fsm.SetCurrentState(nextState);
                    break;
                case Directions.EAST:
                    nextid = Directions.SOUTH;
                    nextState = m_fsm.GetState(nextid);
                    m_fsm.SetCurrentState(nextState);
                    break;
                case Directions.SOUTH:
                    nextid = Directions.WEST;
                    nextState = m_fsm.GetState(nextid);
                    m_fsm.SetCurrentState(nextState);
                    break;
                case Directions.WEST:
                    nextid = Directions.UP;
                    nextState = m_fsm.GetState(nextid);
                    m_fsm.SetCurrentState(nextState);
                    break;
            }
        }

        if (m_gameObject != null)
        {
            switch (m_Directions)
            {
                case Directions.NORTH:
                    m_gameObject.transform.position += Vector3.forward * 3 * Time.deltaTime;
                    break;
                case Directions.EAST:
                    m_gameObject.transform.position += Vector3.right * Time.deltaTime;
                    break;
                case Directions.SOUTH:
                    m_gameObject.transform.position += Vector3.back * 3 * Time.deltaTime;
                    break;
                case Directions.WEST:
                    m_gameObject.transform.position += Vector3.left * Time.deltaTime;
                    break;
            }
        }
    }
}
