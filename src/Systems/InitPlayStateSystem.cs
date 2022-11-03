using Explore.Core;
using Explore.Nodes.Actors;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class InitPlayStateSystem : ISystem
{
    public World World { get; set; }
    public void Run()
    {
        var state = World.GetElement<GameState>();
        
        var game = GD.Load<PackedScene>("src/Nodes/Game.tscn").Instance<Game>();
        var camera = GD.Load<PackedScene>("src/Nodes/Region/RegionCamera.tscn").Instance<RegionCamera>();
        var player = GD.Load<PackedScene>("src/Nodes/Actors/Player.tscn").Instance<Player>();

        camera.Target = player;
        
        game.AddChild(camera);
        game.AddChild(player);
        
        state.AddChild(game);

        World.Spawn(player);

        World.AddElement(game);
        World.AddElement(camera);
        World.AddElement(player);
        
        World.Send(new LoadRegion { Region = "Mountains", Exit = -1 });
    }
}