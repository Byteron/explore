using Explore.Components;
using Explore.Nodes.Actors;
using Explore.Nodes.Physics;
using Godot;
using RelEcs;
using RelEcs.Godot;

namespace Explore.Systems;

public class EnemyAttackSystem : Object, ISystem
{
    public void Run(Commands commands)
    {
        commands.Receive((Spawned spawned) =>
        {
            GD.Print("Spawned!");
            if (!spawned.Entity.TryGet<Enemy>(out var enemy)) return;

            var e = new Marshallable<Entity>(spawned.Entity);
            var c = new Marshallable<Commands>(commands);
            var args = new Godot.Collections.Array { enemy, e, c };

            enemy.Connect(nameof(Enemy.Contacted), this, nameof(OnEnemyContacted), args);
            
            GD.Print("Connected!");
        });
    }

    void OnEnemyContacted(Area2D area, Node2D enemy, Marshallable<Entity> enemyEntity, Marshallable<Commands> commands)
    {
        GD.Print("Contacted!");

        var strength = enemyEntity.Value.Get<Strength>();
        
        var entity = ((Marshallable<Entity>)area.Owner.GetMeta("Entity")).Value;
        var node = (Node2D)area.Owner;
        
        commands.Value.Send(new Damage(entity, strength.Value, enemy.Position.DirectionTo(node.Position)));
    }
}