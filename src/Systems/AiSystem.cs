using Explore.Components;
using Explore.Nodes.Actors;
using RelEcs;

namespace Explore.Systems;

public class Target
{
    public Player Player;
}

public class AiSystem : GodotSystem
{
    public override void Run()
    {
        // get player node, which is sort of a unique component
        if (!TryGetElement<Player>(out var player)) return;
        
        // check if any AI needs to have their target removed
        foreach (var (entity, enemy, vision) in QueryBuilder<Entity, Enemy, Vision>().Has<Target>().Build())
        {
            var distance = player.Position.DistanceTo(enemy.Position);
            if (distance <= vision.Value * 1.2f && distance > 24) continue;
            On(entity).Remove<Target>();
        }
        
        // check if any AI needs to have a target added
        foreach (var (entity, enemy, vision) in QueryBuilder<Entity, Enemy, Vision>().Not<Target>().Build())
        {
            if (player.Position.DistanceTo(enemy.Position) > vision.Value) continue;
            On(entity).Add(new Target { Player = player });
        }
        
        // for every AI with target, move towards the target
        foreach (var (enemy, vel, speed, target) in Query<Enemy, Velocity, Speed, Target>())
        {
            var direction = enemy.GlobalPosition.DirectionTo(target.Player.GlobalPosition);
            vel.Value = direction * speed.Value;    
        }
        
        // if AI doesn't have a target, move back to it's original position
        foreach (var (enemy, vel, speed) in QueryBuilder<Enemy, Velocity, Speed>().Not<Target>().Build())
        {
            var direction = enemy.GlobalPosition.DirectionTo(enemy.Origin);
            vel.Value = direction * speed.Value;    
        }
    }
}