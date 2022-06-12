using Explore.Core;
using Explore.States;
using Godot;

namespace Explore;

public class Main : Node
{
    public override void _Ready()
    {
        var gsc = new GameStateController();
        AddChild(gsc);
        gsc.PushState(new PlayState());
    }
}