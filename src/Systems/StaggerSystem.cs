using Explore.Components;
using Explore.Core;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class StaggerSystem : ISystem
{
    public void Run(Commands commands)
    {
        if (!commands.TryGetElement<DeltaTime>(out var deltaTime)) return;
        
        foreach (var stagger in commands.Query<Stagger>())
        {
            stagger.Value = Mathf.Clamp(stagger.Value - deltaTime.Value, 0f, stagger.Value);
        }
    }
}