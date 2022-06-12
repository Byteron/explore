using Explore.Core;
using Explore.Systems;

namespace Explore.States;

public class PlayState : GameState
{
    public override void Init(GameStateController gameStates)
    {
        InitSystems.Add(new InitPlayStateSystem());

        UpdateSystems
            .Add(new ChangeRegionSystem())
            .Add(new UnloadRegionSystem())
            .Add(new LoadRegionSystem())
            .Add(new AiSystem())
            .Add(new PlayerAttackSystem())
            .Add(new EnemyAttackSystem())
            .Add(new PlayerMoveSystem())
            .Add(new DamageSystem())
            .Add(new StaggerSystem())
            .Add(new MoveSystem());
    }
}