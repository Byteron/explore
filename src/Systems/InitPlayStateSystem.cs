using Explore.Core;
using Explore.Components;
using Explore.Nodes;
using Godot;
using RelEcs;
using RelEcs.Godot;

namespace Explore.Systems;

public class InitPlayStateSystem : ISystem
{
    public void Run(Commands commands)
    {
        var state = commands.GetElement<GameState>();
        
        var game = GD.Load<PackedScene>("src/Nodes/Game.tscn").Instance<Game>();
        var camera = GD.Load<PackedScene>("src/Nodes/Region/RegionCamera.tscn").Instance<RegionCamera>();
        var player = GD.Load<PackedScene>("src/Nodes/Player.tscn").Instance<Player>();

        camera.Target = player;
        
        game.AddChild(camera);
        game.AddChild(player);
        
        state.AddChild(game);
        
        commands.Spawn(player).Add(new Speed { Value = 72f }).Add<Controllable>();

        commands.AddElement(game);
        commands.AddElement(camera);
        commands.AddElement(player);
        
        commands.Send(new LoadRegion { Region = "Forest", Exit = -1 });
    }
}