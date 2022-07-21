using Explore.Components;
using Explore.Nodes.Actors;
using Explore.Nodes.Physics;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class EnemyAttackSystem : GDSystem
{
    public override void Run()
    {
        foreach (var spawned in Receive<Spawned>())
        {
            if (!TryGetComponent<Enemy>(spawned.Entity, out var enemy)) return;

            var strength = GetComponent<Strength>(spawned.Entity);
            var args = new Godot.Collections.Array { enemy, strength, World };

            enemy.Connect(nameof(Enemy.Contacted), this, nameof(OnEnemyContacted), args);
        }
    }

    public void OnEnemyContacted(Area2D area, Node2D enemy, Strength strength, World world)
    {
        var entity = (Entity)area.Owner.GetMeta("Entity");
        var node = (Node2D)area.Owner;

        world.Send(new Damage(entity, strength.Value, enemy.Position.DirectionTo(node.Position)));
    }
}