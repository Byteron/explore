using Explore.Components;
using Explore.Nodes.Actors;
using Explore.Nodes.Physics;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class PlayerAttackSystem : Reference, ISystem
{
    public World World { get; set; }
    public void Run()
    {
        if (!Input.IsActionJustPressed("attack")) return;

        var query = World.Query<Player, ScanArea2D, Strength, AnimationPlayer>().Has<Controllable>().Build();
        foreach (var (player, scanArea, strength, anim) in query)
        {
            if (anim.IsPlaying()) continue;
            
            Attack(World, player, scanArea, strength, anim);
        }
    }

    async void Attack(World world, Player player, ScanArea2D scanArea, Strength strength, AnimationPlayer anim)
    {
        anim.Play("attack");

        await ToSignal(player, "Strike");
        
        var areas = scanArea.GetOverlappingAreas();

        foreach (Area2D area in areas)
        {
            if (area is not HitArea2D hitArea) continue;

            var meta = hitArea.Owner.GetMeta("Entity");

            if (meta is not Marshallable<Entity> colliderEntity) continue;
            
            var position = (hitArea.Owner as Node2D).Position;
            world.Send(new Damage(colliderEntity.Value, strength.Value, player.Position.DirectionTo(position)));
        }
    }
}