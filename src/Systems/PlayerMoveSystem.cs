using Godot;
using RelEcs;
using Explore.Components;
using Explore.Nodes;
using Explore.Nodes.Actors;
using Explore.Nodes.Physics;

namespace Explore.Systems;

public class PlayerMoveSystem : GodotSystem
{
    public override void Run()
    {
        var query = QueryBuilder<Velocity, ScanArea2D, Speed>().Has<Controllable>().Build();

        foreach (var (vel, scanArea, speed) in query)
        {
            var direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

            if (direction == Vector2.Zero) return;

            scanArea.LookAt(scanArea.GlobalPosition + direction);
            
            vel.Value = direction * speed.Value;
        }
    }
}