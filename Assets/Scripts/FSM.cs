using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    public class State
    {
        protected FSM m_fsm;
        public State(FSM fsm)
        {
            m_fsm = fsm;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
    }

    public class FSM
    {
        protected Dictionary<Directions, State> m_states = new Dictionary<Directions, State>();
        protected State m_currentState;

        public FSM()
        {

        }

        public void AddState(Directions key, State state)
        {
            m_states.Add(key, state);
        }

        public State GetState(Directions key)
        {
            return m_states[key];
        }


        public void SetCurrentState(State state)
        {
            if (m_currentState != null)
            {
                m_currentState.OnExit();
            }
            m_currentState = state;
            if (m_currentState != null)
            {
                m_currentState.OnEnter();
            }
        }

        public void OnUpdate()
        {
            if (m_currentState != null)
            {
                m_currentState.OnUpdate();
            }
        }

        public void OnFixedUpdate()
        {
            if (m_currentState != null)
            {
                m_currentState.OnFixedUpdate();
            }
        }
    }
}