using Explore.Components;
using Godot;
using RelEcs;

namespace Explore.Nodes.Actors;

public class Player : Character, ISpawnable
{
    [Signal]
    public delegate void Strike();
    
    public void Spawn(EntityBuilder entity)
    {
        entity
            .Add(this as KinematicBody2D)
            .Add(new Health(Health))
            .Add(new Speed { Value = Speed })
            .Add(new Strength { Value = Strength })
            .Add(new Velocity())
            .Add(new Force())
            .Add(new Stagger())
            .Add<Controllable>();
    }
}