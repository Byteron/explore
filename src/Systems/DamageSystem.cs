using Explore.Components;
using Explore.Nodes.Actors;
using Godot;
using RelEcs;

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
    public World World { get; set; }
    public void Run()
    {
        foreach (var damage in World.Receive<Damage>(this))
        {
            if (!World.TryGetComponent<Health>(damage.Target, out var health)) return;

            health.Value -= damage.Amount;


            if (World.TryGetComponent<Force>(damage.Target, out var force))
            {
                force.Value = damage.Direction * (damage.Amount * 5 + 500f);
            }


            if (World.TryGetComponent<Stagger>(damage.Target, out var stagger))
            {
                stagger.Value = 0.2f;
            }

            if (health.Value > 0) return;


            if (World.HasComponent<Player>(damage.Target))
            {
                World.GetElement<SceneTree>().ReloadCurrentScene();
                return;
            }

            World.DespawnAndFree(damage.Target);
        }
    }
}