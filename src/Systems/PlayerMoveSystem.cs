using Godot;
using RelEcs;
using Explore.Components;
using Explore.Nodes;
using Explore.Nodes.Physics;

namespace Explore.Systems;

public class PlayerMoveSystem : ISystem
{
    public void Run(Commands commands)
    {
        var query = commands.Query<Player, ScanArea2D, Speed>().Has<Controllable>();
        
        foreach (var (player, scanArea, speed) in query)
        {
            var direction = GetMoveDirection();

            if (direction == Vector2.Zero) return;
                
            scanArea.LookAt(scanArea.GlobalPosition + direction);
            player.MoveAndSlide(direction * speed.Value);
        }
    }

    static Vector2 GetMoveDirection()
    {
        return Input.GetVector("move_left", "move_right", "move_up", "move_down");
    }
}