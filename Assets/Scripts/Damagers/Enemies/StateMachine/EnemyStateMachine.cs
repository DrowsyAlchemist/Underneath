using UnityEngine;
using System;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState _initialState;

    private EnemyState _currentState;

    private void Start()
    {
        if (_initialState == null)
            throw new Exception("There is no initial state.");

        _currentState = _initialState;
        _currentState.Enter();
    }

    private void Update()
    {
        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(EnemyState newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
}
