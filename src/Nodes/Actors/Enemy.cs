using Explore.Components;
using Explore.Nodes.Physics;
using Godot;
using RelEcs;
using RelEcs.Godot;

namespace Explore.Nodes.Actors;

public class Enemy : Character, ISpawnable
{
    [Signal]
    public delegate void Contacted(Area2D area);
    
    [Export] int _vision = 72;

    HitArea2D _hitArea;

    public Vector2 Origin;
    
    public override void _Ready()
    {
        Origin = Position;
        
        _hitArea = GetNode<HitArea2D>("HitArea2D");
    }

    public void Spawn(Entity entity)
    {
        entity
            .Add(this as KinematicBody2D)
            .Add(new Health(Health))
            .Add(new Speed { Value = Speed })
            .Add(new Strength { Value = Strength })
            .Add(new Vision { Value = _vision })
            .Add(new Velocity())
            .Add(new Force())
            .Add(new Stagger());
    }

    void OnHitAreaEntered(Area2D area)
    {
        if (area != _hitArea && area is HitArea2D { Owner: Player })
        {
            EmitSignal(nameof(Contacted), area);
        }
    }
}