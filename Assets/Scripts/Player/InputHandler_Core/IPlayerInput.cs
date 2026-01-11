using UnityEngine;

public interface IPlayerInput
{
    Vector2 Move { get; }
    Vector2 Look { get; }

    InputButton Attack { get; }
    InputButton Interact { get; }
    InputButton Crouch { get; }
    InputButton Jump { get; }
    InputButton Previous { get; }
    InputButton Next { get; }
    InputButton Sprint { get; }
    InputButton Pause { get; }
}

