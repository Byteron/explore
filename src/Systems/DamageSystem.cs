using Explore.Components;
using Explore.Nodes.Actors;
using Godot;
using RelEcs;
using RelEcs.Godot;

namespace Explore.Systems;

public class Damage
{
    public Entity Target;
    public int Amount;
    public Vector2 Direction;

    public Damage(Entity entity, int amount, Vector2 direction)
    {
        Target = entity;
        Amount = amount;
        Direction = direction;
    }
}

public class DamageSystem : ISystem
{
    public void Run(Commands commands)
    {
        commands.Receive((Damage damage) =>
        {
            if (!damage.Target.TryGet<Health>(out var health)) return;

            health.Value -= damage.Amount;

            if (damage.Target.TryGet<Force>(out var force))
            {
                force.Value = damage.Direction * (damage.Amount * 5 + 500f);
            }

            if (damage.Target.TryGet<Stagger>(out var stagger))
            {
                stagger.Value = 0.2f;
            }
            
            GD.Print("Damage Dealt!");

            if (health.Value > 0) return;

            if (damage.Target.Has<Player>())
            {
                commands.GetElement<SceneTree>().ReloadCurrentScene();
                return;
            }
            
            damage.Target.DespawnAndFree();
            GD.Print("DEATH!");
        });
    }
}