using Explore.Core;
using Explore.Nodes.Actors;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class InitPlayStateSystem : GDSystem
{
    public override void Run()
    {
        var state = GetElement<GameState>();
        
        var game = GD.Load<PackedScene>("src/Nodes/Game.tscn").Instance<Game>();
        var camera = GD.Load<PackedScene>("src/Nodes/Region/RegionCamera.tscn").Instance<RegionCamera>();
        var player = GD.Load<PackedScene>("src/Nodes/Actors/Player.tscn").Instance<Player>();

        camera.Target = player;
        
        game.AddChild(camera);
        game.AddChild(player);
        
        state.AddChild(game);

        Spawn(player);

        AddElement(game);
        AddElement(camera);
        AddElement(player);
        
        Send(new LoadRegion { Region = "Mountains", Exit = -1 });
    }
}