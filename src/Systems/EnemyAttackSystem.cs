using Explore.Components;
using Explore.Nodes.Actors;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class EnemyAttackSystem : Reference, ISystem
{
    public World World { get; set; }
    public void Run()
    {
        foreach (var spawned in World.Receive<Spawned>(this))
        {
            if (!World.TryGetComponent<Enemy>(spawned.Entity, out var enemy)) return;

            var strength = World.GetComponent<Strength>(spawned.Entity);
            var args = new Godot.Collections.Array { enemy, strength, new Marshallable<World>(World) };

            enemy.Connect(nameof(Enemy.Contacted), this, nameof(OnEnemyContacted), args);
        }
    }

    public void OnEnemyContacted(Area2D area, Node2D enemy, Strength strength, Marshallable<World> world)
    {
        var entity = (Marshallable<Entity>)area.Owner.GetMeta("Entity");
        var node = (Node2D)area.Owner;

        world.Value.Send(new Damage(entity.Value, strength.Value, enemy.Position.DirectionTo(node.Position)));
    }
}