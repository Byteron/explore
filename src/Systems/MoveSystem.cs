using Explore.Components;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class MoveSystem : ISystem
{
    public World World { get; set; }
    public void Run()
    {
        var query = World.Query<KinematicBody2D, Velocity, Force, Stagger>().Build();
        foreach (var (body, vel, force, stagger) in query)
        {
            body.MoveAndSlide(stagger.Value > 0.01f ? force.Value : vel.Value + force.Value, Vector2.Zero);

            force.Value.x = Mathf.Lerp(force.Value.x, 0f, 0.1f);
            force.Value.y = Mathf.Lerp(force.Value.y, 0f, 0.1f);
            
            vel.Value = Vector2.Zero;
        }
    }
}