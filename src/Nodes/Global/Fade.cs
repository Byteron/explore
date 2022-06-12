using Godot;
using System;

public class Fade : CanvasLayer
{
    [Signal]
    public delegate void Finished();
 
    public static Fade Instance { get; private set; }
    
    ColorRect _rect;
    SceneTreeTween _tween;
    
    public override void _Ready()
    {
        Instance = this;

        _rect = GetNode<ColorRect>("ColorRect");
    }

    public void FadeIn()
    {
        Stop();

        _tween = GetTree().CreateTween();
        _tween.TweenProperty(_rect, "color:a", 0.0f, 0.1f);
        _tween.TweenCallback(this, "emit_signal", new Godot.Collections.Array {"Finished"});
    }
    
    public void FadeOut()
    {
        Stop();
        
        _tween = GetTree().CreateTween();
        _tween.TweenProperty(_rect, "color:a", 1.0f, 0.1f);
        _tween.TweenCallback(this, "emit_signal", new Godot.Collections.Array {"Finished"});
        
    }

    void Stop()
    {
        if (_tween is not null && _tween.IsRunning())
        {
            _tween.Stop();
        }
    }
}
