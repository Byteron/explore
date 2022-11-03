using Explore.Nodes.Region;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class ChangeRegionSystem : Reference, ISystem
{
    public World World { get; set; }
    public void Run()
    {   
        foreach (var _ in World.Receive<RegionLoaded>(this))
        {
            var region = World.GetElement<Region>();
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