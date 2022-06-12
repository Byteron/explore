using Explore.Nodes.Region;
using Godot;
using RelEcs;

namespace Explore.Systems;

public class ChangeRegionSystem : Object, ISystem
{
    Commands _commands;
    
    public void Run(Commands commands)
    {
        _commands = commands;
        
        commands.Receive((RegionLoaded _) =>
        {
            var region = commands.GetElement<Region>();
            region.Connect(nameof(Region.Exited), this, nameof(OnRegionExited));
        });
    }

    async void OnRegionExited(string region, int exit)
    {
        Fade.Instance.FadeOut();
        await ToSignal(Fade.Instance, nameof(Fade.Finished));
        
        _commands.Send(new UnloadRegion());
        _commands.Send(new LoadRegion { Region = region, Exit = exit });
        
        Fade.Instance.FadeIn();
        await ToSignal(Fade.Instance, nameof(Fade.Finished));
    }
}