using Actors.Command.Components;
using Pixeye.Actors;

namespace Actors.Command.Processors
{
    public class SelectionProcessor : Processor, ITickFixed
    {
        private readonly Group<SelectionCommand> selectionCommands = default;
        private GameState gameState;

        public SelectionProcessor()
        {
            gameState = Layer.Get<GameState>();
        }


        public void TickFixed(float dt)
        {
            foreach (var selectionCommandsEntity in selectionCommands)
            {
                var command = selectionCommandsEntity.Get<SelectionCommand>();

                foreach (var gameObject in gameState.selectedActors)
                {
                    gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.SetActive(false);
                }

                gameState.selectedActors.Clear();
                foreach (var gameObject in command.selectedActors)
                {
                    gameState.selectedActors.Add(gameObject);
                }

                foreach (var gameObject in gameState.selectedActors)
                {
                    gameObject.transform.GetChild(gameObject.transform.childCount - 1).gameObject.SetActive(true);
                }

                selectionCommandsEntity.Remove<SelectionCommand>();
            }
        }
    }
}
