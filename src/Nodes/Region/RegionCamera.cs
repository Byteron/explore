using Godot;
using System;

public class RegionCamera : Camera2D
{
    public Node2D Target;

    public override void _Process(float delta)
    {
        Position = Target.Position;
    }
}
