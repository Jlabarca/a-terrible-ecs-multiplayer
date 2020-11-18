using Leopotam.Ecs;

namespace LeoECS.Command
{
    public class CommandSystem : IEcsRunSystem
    {
        private GameState gameState;
        private EcsFilter<CommandComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var commandComponent = ref _filter.Get1(index);
                gameState.commandsManager.AddCommand(commandComponent.command);
            }

            gameState.commandsManager.ExecuteCommand();
        }
    }
}
