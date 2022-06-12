using Explore.Components;
using Explore.Nodes.Actors;
using RelEcs;

namespace Explore.Systems;

public class Target
{
    public Player Player;
}

public class AiSystem : ISystem
{
    public void Run(Commands commands)
    {
        if (!commands.TryGetElement<Player>(out var player)) return;
        
        foreach (var (entity, enemy, vision) in commands.Query<Entity, Enemy, Vision>().Has<Target>())
        {
            var distance = player.Position.DistanceTo(enemy.Position);
            if (distance <= vision.Value * 1.5f && distance > 24) continue;
            entity.Remove<Target>();
        }

        foreach (var (entity, enemy, vision) in commands.Query<Entity, Enemy, Vision>().Not<Target>())
        {
            if (player.Position.DistanceTo(enemy.Position) > vision.Value) continue;
            entity.Add(new Target { Player = player });
        }

        foreach (var (enemy, vel, speed, target) in commands.Query<Enemy, Velocity, Speed, Target>())
        {
            var direction = enemy.GlobalPosition.DirectionTo(target.Player.GlobalPosition);
            vel.Value = direction * speed.Value;    
        }
    }
}