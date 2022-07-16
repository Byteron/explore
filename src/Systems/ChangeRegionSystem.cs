using Explore.Nodes.Region;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class ChangeRegionSystem : GodotSystem
{   
    static readonly Functions F = new Functions();

    public override void Run()
    {
        F.World = World;
        
        Receive((RegionLoaded _) =>
        {
            var region = GetElement<Region>();
            region.Connect(nameof(Region.Exited), F, nameof(F.OnRegionExited));
        });
    }

    class Functions : Object
    {
        public World World;
        
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
}