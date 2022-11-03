using Explore.Components;
using Explore.Nodes.Region;
using RelEcs;

namespace Explore.Systems;

public class UnloadRegion
{
}

public class UnloadRegionSystem : ISystem 
{
    public World World { get; set; }
    public void Run()
    {
        foreach (var t in World.Receive<UnloadRegion>(this))
        {
            var game = World.GetElement<Game>();
            var region = World.GetElement<Region>();
            
            game.RemoveChild(region);
            region.QueueFree();

            foreach (var entity in World.Query().Has<IsSpawned>().Build())
            {
                World.DespawnAndFree(entity);
            }
        }
    }
}