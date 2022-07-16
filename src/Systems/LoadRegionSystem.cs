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

public class LoadRegionSystem : GodotSystem 
{
    public override void Run()
    {
        Receive((LoadRegion t) =>
        {
            var game = GetElement<Game>();
            var player = GetElement<Player>();
            var camera = GetElement<RegionCamera>();
            
            var region = GD.Load<PackedScene>($"data/regions/{t.Region}.tscn").Instance<Region>();
            
            game.AddChild(region);
            
            AddOrReplaceElement(region);

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
                var entity = Spawn(instance).Add<IsSpawned>().Id();
                Send(new Spawned { Entity = entity });
            }
            
            Send(new RegionLoaded());
        });
    }
}