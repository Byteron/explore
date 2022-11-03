using Explore.Components;
using Explore.Core;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class StaggerSystem : ISystem
{
    public World World { get; set; }
    public void Run()
    {
        if (!World.TryGetElement<DeltaTime>(out var deltaTime)) return;
        
        foreach (var stagger in World.Query<Stagger>().Build())
        {
            stagger.Value = Mathf.Clamp(stagger.Value - deltaTime.Value, 0f, stagger.Value);
        }
    }
}