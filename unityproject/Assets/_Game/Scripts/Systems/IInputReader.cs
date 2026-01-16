using System;
using UnityEngine;

public interface IInputReader
{
    float MoveValue { get; }
    event Action LaunchPerformed;
    
    void Enable();
    void Disable();
}
