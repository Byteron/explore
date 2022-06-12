using Explore.Components;
using Explore.Nodes.Region;
using Godot;
using RelEcs;
using RelEcs.Godot;

namespace Explore.Systems;

public class UnloadRegion
{
}

public class UnloadRegionSystem : ISystem 
{
    public void Run(Commands commands)
    {
        commands.Receive((UnloadRegion t) =>
        {
            var game = commands.GetElement<Game>();
            var region = commands.GetElement<Region>();
            
            game.RemoveChild(region);
            region.QueueFree();

            var query = commands.Query<Entity, Root>().Has<Spawned>();
            
            foreach (var (entity, root) in query)
            {
                GD.Print("Despawned ", root.Node.Name);
                entity.DespawnAndFree();
            }
            
            GD.Print("Unload level");
        });
    }
}