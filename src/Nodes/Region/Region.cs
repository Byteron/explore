using Godot;
using Godot.Collections;

namespace Explore.Nodes.Region;

public class Region : YSort
{
    [Signal]
    public delegate void Exited(string region, int exit);
    
    public Vector2 SpawnPosition;
    public Vector2 Size;
    
    public Array<Exit> Exits = new();
    public Array<Spawner> Spawners = new();
    
    public override void _Ready()
    {
        SpawnPosition = GetNode<Position2D>("Spawn").GlobalPosition;

        Size = GetNode<TileMap>("Tiles/Ground").GetUsedRect().Size * 16f;
        
        foreach (Exit exit in GetNode("Exits").GetChildren())
        {
            exit.Connect(nameof(Exit.Exited), this, nameof(OnExitExited));
            Exits.Add(exit);
        }
        
        foreach (Spawner spawner in GetNode("Spawners").GetChildren())
        {
            Spawners.Add(spawner);
        }
    }

    void OnExitExited(string region, int exit)
    {
        EmitSignal(nameof(Exited), region, exit);
    }
}