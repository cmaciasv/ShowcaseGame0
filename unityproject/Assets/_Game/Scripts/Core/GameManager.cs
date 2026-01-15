using UnityEngine;
using Zenject;
using System.Collections.Generic;

namespace Showcase.Core
{
    public class GameManager : IInitializable, ITickable
    {
        private IGameState _currentState;
        
        public void Initialize()
        {
            Debug.Log("GameManager Initialized");
            // Set initial state here
        }

        public void Tick()
        {
            _currentState?.Update();
        }

        public void ChangeState(IGameState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }

    public interface IGameState
    {
        void Enter();
        void Update();
        void Exit();
    }
}
