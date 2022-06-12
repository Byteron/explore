using Explore.Nodes.Actors;
using Godot;

namespace Explore.Nodes.Region;

public class Exit : Area2D
{
    [Signal] public delegate void Exited(string region, int exit);

    [Export] public string NextRegion = "Forest";
    [Export] public int NextExit;
    
    void OnBodyEntered(Node body)
    {
        if (body is Player) EmitSignal(nameof(Exited), NextRegion, NextExit);
    }
}