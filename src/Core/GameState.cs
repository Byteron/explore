using Godot;
using RelEcs;

namespace Explore.Core;

public class GameState : Node2D
{
    public readonly SystemGroup InitSystems = new();
    public readonly SystemGroup UpdateSystems = new();
    public readonly SystemGroup ContinueSystems = new();
    public readonly SystemGroup PauseSystems = new();
    public readonly SystemGroup ExitSystems = new();

    public virtual void Init(GameStateController gameStates) { }
}