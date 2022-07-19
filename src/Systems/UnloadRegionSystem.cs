using Explore.Components;
using Explore.Nodes.Region;
using RelEcs;

namespace Explore.Systems;

public class UnloadRegion
{
}

public class UnloadRegionSystem : GodotSystem 
{
    public override void Run()
    {
        foreach (var t in Receive<UnloadRegion>())
        {
            var game = GetElement<Game>();
            var region = GetElement<Region>();
            
            game.RemoveChild(region);
            region.QueueFree();

            foreach (var entity in QueryBuilder().Has<IsSpawned>().Build())
            {
                DespawnAndFree(entity);
            }
        }
    }
}