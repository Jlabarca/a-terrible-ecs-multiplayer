using Actors.Command.Components;
using Actors.Components;
using Pixeye.Actors;

namespace Actors.Command.Processors
{
    public class SelectionProcessor : Processor, ITickFixed
    {
        private readonly Group<SelectionCommand> selectionCommands = default;
        private readonly Group<UnitComponent> units = default;
        private readonly GameState gameState;

        public SelectionProcessor()
        {
            gameState = Layer.Get<GameState>();
        }

        public void TickFixed(float dt)
        {
            foreach (var selectionCommandsEntity in selectionCommands)
            {
                var command = selectionCommandsEntity.Get<SelectionCommand>();
                gameState.selectedActors.Clear();
                gameState.selectedActorsGameObjects.Clear();

                foreach (var ent in units)
                {
                    if (command.selectedActors.Contains(ent.transform.gameObject))
                    {
                        ent.transform.GetChild(ent.transform.childCount - 1).gameObject.SetActive(true);
                        gameState.selectedActors.Add(ent.Get<UnitComponent>().unitId);
                        gameState.selectedActorsGameObjects.Add(ent.transform.gameObject);
                    }
                    else
                        ent.transform.GetChild(ent.transform.childCount - 1).gameObject.SetActive(false);
                }

                selectionCommandsEntity.Remove<SelectionCommand>();
            }
        }
    }
}
