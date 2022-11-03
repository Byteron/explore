using Explore.Components;
using Explore.Nodes.Actors;
using Explore.Nodes.Region;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class LoadRegion
{
    public string Region;
    public int Exit;
}

public class RegionLoaded { }

public class Spawned
{
    public Entity Entity;
}

public class LoadRegionSystem : ISystem 
{
    public World World { get; set; }
    public void Run()
    {
        foreach (var t in World.Receive<LoadRegion>(this))
        {
            var game = World.GetElement<Game>();
            var player = World.GetElement<Player>();
            var camera = World.GetElement<RegionCamera>();
            
            var region = GD.Load<PackedScene>($"data/regions/{t.Region}.tscn").Instance<Region>();
            
            game.AddChild(region);
            
            World.AddOrReplaceElement(region);

            camera.LimitTop = 0;
            camera.LimitLeft = 0;
            camera.LimitBottom = (int)region.Size.y;
            camera.LimitRight = (int)region.Size.x;
            
            player.Position = t.Exit >= 0 ? region.Exits[t.Exit].Position : region.SpawnPosition;

            foreach (var spawner in region.Spawners)
            {
                var instance = spawner.Scene.Instance<Node2D>();
                instance.Position = spawner.Position;
                game.AddChild(instance);
                var entity = World.Spawn(instance).Add<IsSpawned>().Id();
                World.Send(new Spawned { Entity = entity });
            }
            
            World.Send(new RegionLoaded());
        }
    }
}