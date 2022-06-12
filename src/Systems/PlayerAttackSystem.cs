using Explore.Components;
using Explore.Nodes.Actors;
using Explore.Nodes.Physics;
using Godot;
using RelEcs;
using RelEcs.Godot;

namespace Explore.Systems;

public class PlayerAttackSystem : ISystem
{
    public void Run(Commands commands)
    {
        if (!Input.IsActionJustPressed("attack")) return;

        var query = commands.Query<Player, ScanArea2D, Strength>().Has<Controllable>();
        foreach (var (player, scanArea, strength) in query)
        {
            GD.Print("Attack!");

            var areas = scanArea.GetOverlappingAreas();

            foreach (Area2D area in areas)
            {
                if (area is not HitArea2D hitArea) continue;

                var meta = hitArea.Owner.GetMeta("Entity");

                if (meta is not Marshallable<Entity> colliderEntity) continue;

                var position = (hitArea.Owner as Node2D).Position;
                commands.Send(new Damage(colliderEntity.Value, strength.Value, player.Position.DirectionTo(position)));
            }
        }
    }
}