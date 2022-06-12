using Godot;

namespace Explore.Nodes.Actors;

public class Character : KinematicBody2D
{
    [Export] protected int Health = 100;
    [Export] protected int Strength = 10;
    [Export] protected float Speed = 72;
}