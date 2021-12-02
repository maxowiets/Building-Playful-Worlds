using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyPatterns
{
    public abstract class State
    {
        protected FSM m_fsm;
        public State(FSM fsm)
        {
            m_fsm = fsm;
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnUpdate();
        public abstract void OnFixedUpdate();
    }

    public class FSM
    {
        protected Dictionary<System.Type, State> m_states = new Dictionary<System.Type, State>();
        protected State m_currentState;

        public FSM()
        {

        }

        public void AddState(System.Type key, State state)
        {
            m_states.Add(key, state);
        }

        public State GetState(System.Type key)
        {
            return m_states[key];
        }


        public void SetCurrentState(State state)
        {
            m_currentState?.OnExit();
            m_currentState = state;
            m_currentState?.OnEnter();
        }

        public void OnUpdate()
        {
            m_currentState?.OnUpdate();
        }

        public void OnFixedUpdate()
        {
            m_currentState?.OnFixedUpdate();
        }
    }
}
