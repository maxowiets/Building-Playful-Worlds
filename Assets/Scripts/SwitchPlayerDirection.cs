using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class SwitchPlayerDirection : MonoBehaviour
{
     private FSM m_fsm = new FSM();

    void Start()
    {
        m_fsm.AddState(Directions.NORTH, new MovementStates(m_fsm, this.gameObject));
        m_fsm.AddState(Directions.EAST, new MovementStates(m_fsm, this.gameObject, Directions.EAST));
        m_fsm.AddState(Directions.SOUTH, new MovementStates(m_fsm, this.gameObject, Directions.SOUTH));
        m_fsm.AddState(Directions.WEST, new MovementStates(m_fsm, this.gameObject, Directions.WEST));
        m_fsm.AddState(Directions.UP, new MovementUpState(m_fsm, this.gameObject));

        m_fsm.SetCurrentState(m_fsm.GetState(Directions.NORTH));
    }

    // Update is called once per frame
    void Update()
    {
        if (m_fsm != null)
        {
            m_fsm.OnUpdate();
        }
    }
}
