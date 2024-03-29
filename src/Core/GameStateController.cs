global using World = RelEcs.World;
using System.Collections.Generic;
using Godot;

namespace Explore.Core;

public class GodotInput
{
    public InputEvent Event;
}

public class DeltaTime
{
    public float Value;
}

public class GameStateController : Node
{
    public readonly World World = new();
        
    readonly Stack<GameState> _stack = new();

    public GameStateController()
    {
        World.AddElement(this);
        World.AddElement(new DeltaTime());
    }

    public override void _Ready()
    {
        Name = "GameStateController";
        World.AddElement(GetTree());
    }

    public override void _UnhandledInput(InputEvent e)
    {
        if (_stack.Count == 0)
        {
            return;
        }
        
        World.Send(e);
    }

    public override void _Process(float delta)
    {
        if (_stack.Count == 0)
        {
            return;
        }

        var currentState = _stack.Peek();
        World.GetElement<DeltaTime>().Value = delta;
        currentState.UpdateSystems.Run(World);

        World.Tick();
    }

    public override void _ExitTree()
    {
        foreach (var state in _stack)
        {
            state.ExitSystems.Run(World);
        }
    }

    public void PushState(GameState newState)
    {
        CallDeferred(nameof(PushStateDeferred), newState);
    }

    public void PopState()
    {
        CallDeferred(nameof(PopStateDeferred));
    }

    public void ChangeState(GameState newState)
    {
        CallDeferred(nameof(ChangeStateDeferred), newState);
    }

    void PopStateDeferred()
    {
        if (_stack.Count == 0) return;

        var currentState = _stack.Pop();
        currentState.ExitSystems.Run(World);
        RemoveChild(currentState);
        currentState.QueueFree();

        if (_stack.Count <= 0) return;
            
        currentState = _stack.Peek();
        World.ReplaceElement(currentState);
        currentState.ContinueSystems.Run(World);
    }

    void PushStateDeferred(GameState newState)
    {
        if (_stack.Count > 0)
        {
            var currentState = _stack.Peek();

            if (currentState.GetType() == newState.GetType())
            {
                GD.PrintErr($"{currentState.GetType().ToString()} already at the top of the stack!");
                return;
            }

            currentState.PauseSystems.Run(World);
        }

        newState.Name = newState.GetType().Name;
        _stack.Push(newState);
        AddChild(newState);
        
        if (World.HasElement<GameState>()) World.ReplaceElement(newState);
        else World.AddElement(newState);
        
        newState.Init(this);
        newState.InitSystems.Run(World);
    }

    void ChangeStateDeferred(GameState newState)
    {
        if (_stack.Count > 0)
        {
            var currentState = _stack.Pop();
            currentState.ExitSystems.Run(World);
            RemoveChild(currentState);
            currentState.QueueFree();
        }

        newState.Name = newState.GetType().Name;
        _stack.Push(newState);
        AddChild(newState);
        World.ReplaceElement(newState);
        newState.Init(this);
        newState.InitSystems.Run(World);
    }
}