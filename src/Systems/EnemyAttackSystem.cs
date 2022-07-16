using Explore.Components;
using Explore.Nodes.Actors;
using Explore.Nodes.Physics;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class EnemyAttackSystem : GodotSystem
{
    Functions F = new Functions();

    public override void Run()
    {
        Receive((Spawned spawned) =>
        {
            if (!TryGetComponent<Enemy>(spawned.Entity, out var enemy)) return;

            var strength = GetComponent<Strength>(spawned.Entity);
            var args = new Godot.Collections.Array { enemy, strength, new Marshallable<World>(World) };

            enemy.Connect(nameof(Enemy.Contacted), F, nameof(Functions.OnEnemyContacted), args);
        });
    }

    class Functions : Object
    {
        public void OnEnemyContacted(Area2D area, Node2D enemy, Strength strength, Marshallable<World> world)
        {
            var entity = ((Marshallable<Entity>)area.Owner.GetMeta("Entity")).Value;
            var node = (Node2D)area.Owner;

            world.Value.Send(new Damage(entity, strength.Value, enemy.Position.DirectionTo(node.Position)));
        }
    }
}