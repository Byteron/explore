using Explore.Nodes.Region;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class ChangeRegionSystem : GDSystem
{
    public override void Run()
    {   
        foreach (var _ in Receive<RegionLoaded>())
        {
            var region = GetElement<Region>();
            region.Connect(nameof(Region.Exited), this, nameof(OnRegionExited));
        }
    }
    
    public async void OnRegionExited(string region, int exit)
    {
        Fade.Instance.FadeOut();
        await ToSignal(Fade.Instance, nameof(Fade.Finished));
        
        World.Send(new UnloadRegion());
        World.Send(new LoadRegion { Region = region, Exit = exit });
        
        Fade.Instance.FadeIn();
        await ToSignal(Fade.Instance, nameof(Fade.Finished));
    }
}