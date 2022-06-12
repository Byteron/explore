using Explore.Components;
using Explore.Nodes;
using Explore.Nodes.Actors;
using Explore.Nodes.Region;
using Godot;
using RelEcs;
using RelEcs.Godot;

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
    public void Run(Commands commands)
    {
        commands.Receive((LoadRegion t) =>
        {
            var game = commands.GetElement<Game>();
            var player = commands.GetElement<Player>();
            var camera = commands.GetElement<RegionCamera>();
            
            var region = GD.Load<PackedScene>($"data/regions/{t.Region}.tscn").Instance<Region>();
            
            game.AddChild(region);
            
            commands.AddOrReplaceElement(region);

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
                var entity = commands.Spawn(instance).Add<IsSpawned>();
                commands.Send(new Spawned { Entity = entity });
            }
            
            commands.Send(new RegionLoaded());
        });
    }
}