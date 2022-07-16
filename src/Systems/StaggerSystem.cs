using Explore.Components;
using Explore.Core;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class StaggerSystem : GodotSystem
{
    public override void Run()
    {
        if (!TryGetElement<DeltaTime>(out var deltaTime)) return;
        
        foreach (var stagger in Query<Stagger>())
        {
            stagger.Value = Mathf.Clamp(stagger.Value - deltaTime.Value, 0f, stagger.Value);
        }
    }
}